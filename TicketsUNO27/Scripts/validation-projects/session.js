
    $(document).ready(function () {
        $("#btn-send-ajax").click(function () {

            var selectFile = ($("#fileUpload")).get(0).files;
            var dataString = new FormData();

            for (var i = 0; i < selectFile.length; i++) {
                dataString.append("fileUpload", selectFile[i]);
            }
            dataString.append("FileName", $("#FileName").val());
            dataString.append("FileDescription", $("#FileDescription").val());
            $.ajax({
                url: '@Url.Action("LoadFile","Files")',
                type: "POST",
                data: dataString,
                contentType: false,
                processData: false,
                async: false,
                success: function (data) {
                    if (typeof (data.Value) != "undefined") {
                        alert(data.Message);
                    } else {
                        setTimeout(function () { location.href = "/Files/Index" }, 2000);
                        alert("Archivo subido con exito")
                    }
                },
                error: function (data) {

                }

            })
        });
    });
