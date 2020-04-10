mntoForms = function () {
    var me = this;

    me.Globals = {
        token: $("#token"),
        lblTitulo: $("#lblTitulo"),
        lblDescripcion: $("#lblDescripcion"),
        lblFecha: $("#lblFecha"),
        idUsuario: $("#idUsuario"),
        idForm: $("#idForm"),
        idEmpresa: $("#idEmpresa"),
    }

    me.Funciones = {
        InicializarEventos: function () {
            console.log(me.Eventos.getUrlParameter("token"))
            me.Globals.token.val(me.Eventos.getUrlParameter("token"));
            me.Eventos.FormOnload();

            $("#btnGuardar").unbind().click(function () { me.Eventos.ValidarFormulario() });


          
            //btnGuardar
        },
        FormSaveTrx: function (formData) {
            $.ajax({
                type: 'POST',
                url: "Home/FormSave",
                dataType: 'json',
                data: JSON.stringify(formData),
                contentType: 'application/json; charset=utf-8',
                async: false,
                beforeSend: function () {
                    //  showLoader();
                },
                success: function (response) {
                    console.log(response);
                    if (response.success == true) {

                        $("#lblSubTitulo").hide();
                        $("#lblFecha").hide();
                        $("#btnGuardar").hide();

                        $("#error").hide();
                        $("#success").show();
                        $("#lblTitlesuccess").html(response.responseGetForm.mensaje);

                        $("#div_preview").empty();
                       

                    } else {

                        $("#lblSubTitulo").hide();
                        $("#lblFecha").hide();
                        $("#btnGuardar").hide();

                        $("#success").hide();
                        $("#error").show();
                        $("#lblTitleerror").html(response.responseGetForm.mensaje);

                        $("#div_preview").empty();
                       
                    }
                },
            });
        },
        FormOnloadTrx: function (formData) {
            $.ajax({

                type: 'POST',
                url: "Home/FormOnloadAsync",
                dataType: 'json',
                data: JSON.stringify(formData),
                contentType: 'application/json; charset=utf-8',
                async: false,
                beforeSend: function () {
                    //  showLoader();
                },
                success: function (result) {
                    console.log(result);

                   response = result.responseGetForm;
                    me.Globals.lblTitulo.text(response.form.titulo);
                    me.Globals.lblDescripcion.text(response.form.comentario);
                    me.Globals.lblFecha.text(response.form.fecha_vigencia);
                    me.Globals.idForm.val(response.form.idform);
                    me.Globals.idUsuario.val(response.form.idusuario);
                    me.Globals.idEmpresa.val(response.form.idempresa);

                    newObj = response.form.controls;
                    var myJSON = JSON.stringify(newObj);
                    window.sessionStorage.setItem("controls", myJSON);
                    var str_html = "";
                    var cont_pregunta = 1;
                    for (var i = 0; i < newObj.length; i++) {
                        var objeto = newObj[i];
                        var respuesta_obligatoria = objeto.respuesta_obligatoria;
                        var class_required = "";
                        if (respuesta_obligatoria == "1")
                            class_required = "  required";
                        switch (objeto.tipo) {
                            case "1":
                                str_html_controls = "";

                                for (var j = 0; j < objeto.opciones.length; j++) {
                                    if (objeto.tipo_respuesta == '1') {
                                        str_html_controls = str_html_controls + "  <div class='i-checks'><label> <input type='radio' data-control='radio' class='" + class_required +"' value='" + objeto.opciones[j]["descripcion"] + "' name='rdb" + objeto.id + "'> <i></i> " + objeto.opciones[j]["descripcion"]  + " </label></div>";
                                    } else if (objeto.tipo_respuesta == '2') {
                                        str_html_controls = str_html_controls + "  <div class='i-checks'><label> <input type='checkbox' data-control='checkbox' class='" + class_required +"' value='" + objeto.opciones[j]["descripcion"] + "' name='ckb" + objeto.id + "'> <i></i> " + objeto.opciones[j]["descripcion"]  + " </label></div>";
                                    } else if (objeto.tipo_respuesta == '3') {
                                        if (j == 0) {
                                            str_html_controls = str_html_controls + "<select class='input--style-6 " + class_required +"' data-control='select' id='cmb" + objeto.id + "' name='cmb" + objeto.id + "'>";
                                            str_html_controls = str_html_controls + " <option>" + objeto.opciones[j]["descripcion"]  + "</option>";
                                        } else if (j == objeto.opciones.length - 1) {
                                            str_html_controls = str_html_controls + " <option>" + objeto.opciones[j]["descripcion"]  + "</option>";
                                            str_html_controls = str_html_controls + "</select>";
                                        } else {
                                            str_html_controls = str_html_controls + " <option>" + objeto.opciones[j]["descripcion"] + "</option>";
                                        }
                                    }
                                }

             

                                str_html = str_html + "<div class='form-row'> " +
                                    "    <label class='name'><h3><b>" + cont_pregunta + ". </b>" + objeto.pregunta + "</h3></label > " +
                                    "    <div class='value'>" + str_html_controls + "</div> " +
                                    "</div>";
                                cont_pregunta++;
                                break;
                            case "2":
                                str_html_controls = "";
                                if (objeto.respuesta_larga == '0') {
                                    if (objeto.restriccion != '0') {
                                        var validacion = "";
                                        switch (objeto.restriccion) {
                                            case "2":
                                                validacion = " min='" + (parseInt(objeto.valor1) + 1) + "'";
                                                break;
                                            case "3":
                                                validacion = " min='" + parseInt(objeto.valor1)  + "'";
                                                break;
                                            case "4":
                                                validacion = " max='" + (parseInt(objeto.valor1) - 1) + "'";
                                                break;
                                            case "5":
                                                validacion = " max='" + parseInt(objeto.valor1)  + "'";
                                                break;
                                            case "6":
                                                validacion = " min='" + (parseInt(objeto.valor1) ) + "'" + " max='" + (parseInt(objeto.valor1) ) + "'";
                                                break;
                                            case "7": //Que no sea igual a
                                                validacion = "";
                                                break;
                                            case "8":
                                                validacion = " min='" + (parseInt(objeto.valor1)) + "'" + " max='" + (parseInt(objeto.valor2)) + "'";
                                                break;
                                            case "9"://Que no esté comprendido entre
                                                validacion = "";
                                                break;
                                            default:
                                                validacion = " ";
                                                break;
                                        }

                                        str_html_controls = "<input type='number' "+validacion+" data-control='text' class='input--style-6" + class_required + "' id='txt" + objeto.id + "' name='txt" + objeto.id + "'>";
                                    } else {
                                        str_html_controls = "<input type='text' data-control='text' class='input--style-6" + class_required + "' id='txt" + objeto.id + "' name='txt" + objeto.id + "'>";
                                    }
                                    
                                } else {
                                    str_html_controls = "<textarea class='textarea--style-6" + class_required +"' data-control='textarea' id='text" + objeto.id + "' text='text" + objeto.id + "'></textarea>";
                                }

                                str_html = str_html + "<div class='form-row'> " +
                                    "    <label class='name'><h3><b>" + cont_pregunta + ". </b>" + objeto.pregunta + "</h3></label > " +
                                    "    <div class='value'>" + str_html_controls + "</div> " +
                                    "</div>";
                                cont_pregunta++;
                                break;
                            case "3":

                                str_html_controls = "";
                                str_html_controls = "<input type='text' data-control='text-fecha' class='input--style-6 datepicker" + class_required +"' id='txtfec" + objeto.id + "' name='txtfec" + objeto.id + "' placeholder='dd/MM/YY'>";


                                str_html = str_html + "<div class='form-row'> " +
                                    "    <label class='name'><h3><b>" + cont_pregunta + ". </b>" + objeto.pregunta + "</h3></label > " +
                                    "    <div class='value'>" + str_html_controls + "</div> " +
                                    "</div>";
                                cont_pregunta++;
                                break;
                            case "4":

                                str_html_controls = "";
                                if (objeto.tipo_simbolo == "1")
                                    str_html_controls = "<div id='" + objeto.id + "' data-control='circle_checked' class='default_circle" + class_required +"' data-number='" + objeto.niveles + "'></div>";
                                else
                                    str_html_controls = "<div id='" + objeto.id + "' data-control='start_checked' class='default_start" + class_required +"' data-number='" + objeto.niveles + "'></div>";


                                str_html = str_html + "<div class='form-row'> " +
                                    "    <label class='name'><h3><b>" + cont_pregunta + ". </b>" + objeto.pregunta + "</h3></label > " +
                                    "    <div class='value'>" + str_html_controls + "</div> " +
                                    "</div>";
                                cont_pregunta++;
                                break;
                            case "5":
                                str_html_controls = "<ul class='list-group clear-list m-t ul_drag_drop' style='font-size:16px!important;' id='" + objeto.id + "' >";

                                for (var j = 0; j < objeto.opciones.length; j++) {
                                    str_html_button_desc = "";
                                    str_html_button_asc = "";
                                    str_html_controls = str_html_controls + "<li class='list-group-item fist-item infont' style='cursor:pointer'>  " + str_html_button_desc + " <span>" + objeto.opciones[j]["descripcion"] + "</span> " + str_html_button_asc + " </li>";
                                }
                                str_html_controls = str_html_controls + "</ul>";

                                str_html = str_html + "<div class='form-row'> " +
                                    "    <label class='name'><h3><b>" + cont_pregunta + ". </b>" + objeto.pregunta + "</h3></label > " +
                                    "    <div class='value'>" + str_html_controls + "</div> " +
                                    "</div>";
                                cont_pregunta++;
                                break;

                            case "6":
                                str_html_controls = " <div class='hr-line-dashed'></div>";
                                str_html = str_html + "<div class='form-group'> " +
                                    "    <label class='name'><h4 style='font-size: 22px!important;'>" + objeto.pregunta + "</h4></label > " +
                                    "     " + str_html_controls + "" +
                                    "</div>";
                                break;
                            default:
                        }


                    }


                    $("#div_preview").empty();
                    $("#div_preview").html(str_html);
                    $('.i-checks').iCheck({
                        checkboxClass: 'icheckbox_square-green',
                        radioClass: 'iradio_square-green',
                    });

                    $('.default_start').raty(
                        {
                            number: function () {
                                return $(this).attr('data-number');
                            },
                            click: function (score, evt) {
                                console.log('ID: ' + this.id + "\nscore: " + score + "\nevent: " + evt.type);
                            },
                            starOff: '/lib/raty-master/images/star-off.png',
                            starOn: '/lib/raty-master/images/star-on.png'
                        }
                    );

                    $('.default_circle').raty(
                        {
                            number: function () {
                                return $(this).attr('data-number');
                            },
                            click: function (score, evt) {
                                console.log('ID: ' + this.id + "\nscore: " + score + "\nevent: " + evt.type);
                            },
                            starOff: '/lib/raty-master/images/circle-off.png',
                            starOn: '/lib/raty-master/images/circle-on.png'
                        }
                    );

                    $(".ul_drag_drop").sortable();

                    $(".datepicker").datepicker({
                        changeMonth: true,
                        changeYear: true,
                        yearRange: "-80:+0",
                        beforeShow: function (input, inst) {
                            var rect = input.getBoundingClientRect();
                            setTimeout(function () {
                                inst.dpDiv.css({ top: rect.top + 40, left: rect.left + 0 });
                            }, 0);
                        }
                    });


                    $("#div_preview").validate({
                        ignore: "",
                        highlight: function (element) {
                            $(element).closest('.control-group').removeClass('success').addClass('error');
                        },
                        success: function (element) {
                            $(element).addClass('valid').closest('.control-group').removeClass('error').addClass('success');
                         
                        },
                    });

                    $(".required").each(function () {
                        var obj_name = "";
                        if ($(this).attr("data-control") == "radio" || $(this).attr("data-control") == "checkbox") {
                            obj_name = $(this).attr("name");


                            $('input[name="' + obj_name + '"]').rules("add", {
                                required: true,
                                messages: {
                                    required: "*Este campo es requerido"
                                }
                            });

                        } else if ($(this).attr("data-control") == "circle_checked" ) {

                            obj_name = $(this).find("input[type='hidden']");
                            id = "circle_checked_" + $(this).attr("id");
                            $(obj_name).attr('id', id);

                            //console.log(id);
                            $("#" + id).rules("add", {
                                required: true,
                                messages: {
                                    required: "*Este campo es requerido"
                                }
                            });

                        } else if ( $(this).attr("data-control") == "start_checked") {

                            obj_name = $(this).find("input[type='hidden']");
                            id = "start_checked_" + $(this).attr("id");
                            $(obj_name).attr('id', id);

                            $(obj_name).attr('id', id);

                            //console.log(id);
                            $("#" + id).rules("add", {
                                required: true,
                                messages: {
                                    required: "*Este campo es requerido"
                                }
                            });

                        }else {
                            obj_name = $(this).attr("id");
                            $("#" + obj_name).rules("add", {
                                required: true,
                                messages: {
                                    required: "*Este campo es requerido"
                                }
                            });
                        }
                        
                    });
                       




                }
            })
        },

    };

    me.Eventos = {
        ValidarFormulario: function () {

            $("#div_preview").valid();
            if (!$("#div_preview").valid()) {
                console.log("SE VALIDO");
                return false;
            }
                


            var entidad = entidadmodel.requestSaveForm();
            var controls = window.sessionStorage.getItem("controls");
            if (controls != null) {
                var newObj = JSON.parse(controls);
                for (var i = 0; i < newObj.length; i++) {
                    var objeto = newObj[i];
                    var respuesta = "";
                    switch (objeto.tipo) {
                        case "1":

                            if (objeto.tipo_respuesta == '1') {
                                respuesta = $("input[name='rdb" + objeto.id+"']:checked").val();
                            }
                            else if (objeto.tipo_respuesta == '2') {
                                //ckb1586799836142
                                var allVals = [];
                                $("input[name='ckb" + objeto.id+"']:checked").each(function () {
                                    allVals.push($(this).val());
                                });
                                respuesta = allVals.toString();
                            }
                            else if (objeto.tipo_respuesta == '3') {
                                respuesta = $("#txt" + objeto.id).val();
                            }

                            break;
                        case "2":
                            if (objeto.respuesta_larga == '0') {
                                respuesta = $("#txt" + objeto.id).val();
                            } else {
                                respuesta = $("#text" + objeto.id).val();
                            }
                            
                            break;
                        case "3":
                            respuesta = $("#txtfec" + objeto.id).val();
                            break;
                        case "4":
                            respuesta = $("#" + objeto.id).find("input[name='score']").val();
                            break;
                        case "5":
                            var allVals = [];
                            $("#" + objeto.id+" li").each(function () {
                               allVals.push($(this).find("span").text());
                            });
                            respuesta = allVals.toString();
                            break;
                    }

                    var obj = new Object();
                    obj.id = objeto.id;
                    obj.value = respuesta;
                    entidad.controls.push(obj);
                 };
            }


            entidad.idEmpresa = me.Globals.idEmpresa.val();
            entidad.idForm = me.Globals.idForm.val();
            entidad.idUsuario = me.Globals.idUsuario.val();
            entidad.token = me.Globals.token.val();
            me.Funciones.FormSaveTrx(entidad);
        },
        FormOnload: function () {
            var entidad = entidadmodel.requestGetForm();
            entidad.token = me.Globals.token.val();
            me.Funciones.FormOnloadTrx(entidad);
        },
        getUrlParameter: function (sParam) {
            var sPageURL = window.location.search.substring(1),
                sURLVariables = sPageURL.split('&'),
                sParameterName,
                i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
                }
            }
            
        }
     };

    me.Inicializar = function () {
        me.Funciones.InicializarEventos();
    };
};



$(document).ready(function () {


    var oForms = new mntoForms();
    oForms.Inicializar();


});

