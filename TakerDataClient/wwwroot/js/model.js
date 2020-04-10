const entidadmodel = new function () {

    this.requestGetForm = function () {
        return {
            token:""           
        };
    };

    this.requestSaveForm = function () {
        return {
            idUsuario: "",
            idEmpresa: "",
            idForm: "",
            token:"",
            controls: new Array(),
        };
    }


    this.oControl = function () {
        return {
            id: "",
            value: ""
        };
    }


    

};


