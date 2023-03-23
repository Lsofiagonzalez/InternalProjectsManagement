using Core.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using tmkhogares.Models;
using tmkhogares.Models.Calificacion;
using tmkhogares.ViewModel;

namespace tmkhogares.Servicios
{
    public class ServicioUsuarios
    {
        private readonly Contexto db = new Contexto();
        public ServicioUsuarios() { }

        /// <summary>
        /// Método que retorna todos los usuarios según las campañas a las que se encuentre asociado
        /// </summary>
        /// <param name="campanasUsuario"></param>
        /// <returns></returns>
        public List<User> RetornarUsuariosPorCampana(List<int> campanasUsuario)
        {
            List<User> usuarios = new List<User>();

            foreach (var item in campanasUsuario)
            {
                List<User> _usuarios = new List<User>();
                _usuarios            = db.Usuario.Where(x => x.UsuarioCampanas.Any(uc => uc.CampanaId == item) 
                                                          && x.Status 
                                                          && x.UserRol.Any(ur => ur.RolId == new Guid("4450491C-32E6-41AE-B301-CB22DFC8BBD9")))
                                                         .Include(x => x.TeamLeader)
                                                         .ToList();
                usuarios.AddRange(_usuarios);
            }

            return usuarios;
        }


        /// <summary>
        /// Médodo que retorna el usuario según el parámetro establecido
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public List<User> RetornarUsuarioPorDocumento(string documento)
        {
            return db.Usuario.OrderBy(u => u.Names).Where(u => u.Document.Contains(documento)).Include(n => n.Nivel).Include(n => n.TeamLeader).ToList();
        }

        /// <summary>
        /// Médodo para validar si el usuario se encuentra registrado en el sistema.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public bool UsuarioExiste(string document)
        {
            int existe = db.Usuario.Where(x => x.Document.Trim() == document).Count();
            if (existe > 0) return true;

            return false;
        }


        /// <summary>
        /// Médodo que retorna todos los usuarios con perfil de Calidad y que han realizado monitoreos.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public List<User> ObtenerListadoAutitores(int campanaid)
        {
            List<User> usuarios = new List<User>();
            var lista = db.Calificacions.Include(x => x.Usuario)
                                        .Where(x => x.CampañaId == campanaid && x.Usuario.Status)
                                        .ToList();

           var listaId = (from d in lista
                        group d by new { d.UsuarioCalidadId } into data
                        select new User()
                        {                            
                            Id       = data.Key.UsuarioCalidadId
                           
                        }).ToList();

            foreach (var item in listaId)
            {
                usuarios.Add(db.Usuario.Where(x => x.Id == item.Id).FirstOrDefault());
            }

             

            return usuarios;


        }

        /// <summary>
        /// Médodo que retorna aquellos usuarios auditados, filtrando por auditor.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public (List<AuditadosVM>, ResumenFeedBackVM) IndexListadoAuditados(Guid usuarioId, int? estado)
        {
            List<AuditadosVM> usuarios = new List<AuditadosVM>();
            List<Calificacion> lista = new List<Calificacion>();
           

            if (estado > 0)
            {
                lista = db.Calificacions.OrderByDescending(x => x.CreatedAt).Include(x => x.Usuario)
                                        .Where(x => x.UsuarioCalidadId == usuarioId && x.Usuario.Status && x.CompromisoAsesor == string.Empty)
                                        .ToList();
            }else if (estado == 0)
            {
                lista = db.Calificacions.OrderByDescending(x => x.CreatedAt).Include(x => x.Usuario)
                                        .Where(x => x.UsuarioCalidadId == usuarioId && x.Usuario.Status  && x.CompromisoAsesor != string.Empty)
                                        .ToList();
            }
            else
            {
                lista = db.Calificacions.OrderByDescending(x => x.CreatedAt).Include(x => x.Usuario)
                                        .Where(x => x.UsuarioCalidadId == usuarioId && x.Usuario.Status)
                                        .ToList();
            }   
           
            foreach (var item in lista)
            {
                var usuario = db.Usuario.Include(x => x.TeamLeader).Where(x => x.Id == item.AsesorId).FirstOrDefault();              

                usuarios.Add(new AuditadosVM()
                {
                    Id = usuario.Id,
                    Documento = usuario.Document,
                    Nombre = usuario.Names,
                    TeamLeader = usuario.TeamLeader == null ? "" : usuario.TeamLeader.Names,
                    CalificacionId = item.Id,
                    FormularioId = item.FormularioId,
                    AuditorId = item.UsuarioCalidadId,
                    CampanaId = item.CampañaId,
                    FechaMonitoreo = item.CreatedAt
                  
                });
            }

            var totales = new ResumenFeedBackVM()
            {
                PendientesFeedBack = ObtenerTotalFeedBack(usuarioId).Item1,
                NoPendientesFeedBack = ObtenerTotalFeedBack(usuarioId).Item2
            };
            
           
            return (usuarios, totales);

        }

        /// <summary>
        /// Médodo que retorna el usuario según el parámetro establecido
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public User RetornarUsuarioPorId(Guid id)
        {
            return db.Usuario.Where(u => u.Id == id).Include(n => n.TeamLeader).FirstOrDefault();
        }

        /// <summary>
        /// Médodo que retorna el total de usuarios pendientes y no pendientes de Feedback.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public (int, int) ObtenerTotalFeedBack(Guid usuarioId)
        {
            var total_1 = db.Calificacions.OrderByDescending(x => x.CreatedAt).Include(x => x.Usuario)
                                        .Where(x => x.UsuarioCalidadId == usuarioId && x.Usuario.Status && x.CompromisoAsesor == string.Empty)
                                        .Count();

            var total_2 = db.Calificacions.OrderByDescending(x => x.CreatedAt).Include(x => x.Usuario)
                                        .Where(x => x.UsuarioCalidadId == usuarioId && x.Usuario.Status && x.CompromisoAsesor != string.Empty)
                                        .Count();

            return (total_1, total_2);

        }
    }
}