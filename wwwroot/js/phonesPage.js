var app = new Vue
    ({
        el: '#body-div',

        data:
        {
            //Коллекция телефонов
            phones: [],
            url:
            {
                phones: '/Home/GetPhones'
            },
            //Форма дабвления
            addArea:
            {
                visibility: false
            },
            //Форма редактирвоания
            updateArea:
            {
                visibility: false
            },
            //Редатируемая запись
            editPhone:
            {
                fname: '',
                lname: '',
                mname: '',
                home_number_phone: '',
                mobile_number_phone: '',
                city: '',
                street_number_house: '',
                number_apartment: ''
            },
            idremovePhone:0,
            //Добавляемая запись
            addPhone:
            {
                fname: '',
                lname: '',
                mname: '',
                home_number_phone: '',
                mobile_number_phone: '',
                city: '',
                street_number_house: '',
                number_apartment: ''

            },
            //Модальное окно на редактирование
            showEditModal: false,
            //Модальное окно на удаление
            showRemoveModal: false

        },
        mounted() {
            this.addArea.visibility = false;
            this.updateArea.visibility = false;
            this.getPhonesFunc();
        },
        methods:
        {
            //Получние коллекции phones
            getPhonesFunc: function () {
                axios.get(this.url.phones).then((response) => {
                    this.phones = response.data;
                });
            },
            //Добавление записи в коллекцию phones
            sendPhoneFunc: function () {

                axios.post('/Home/AddPhone', this.addPhone)
                    .then(response => {
                        console.log(response.data);
                        this.getPhonesFunc();
                    });
            },
            //Экспорт данных в CSV файл
            exportPhones: function () {
                axios.get('/Home/ExportPhonesToCSV');
            },
            //Изменеие данных объекта phone в коллекции phones
            editPhoneFunc: function () {
                axios.post('/Home/EditPhone', this.editPhone)
                    .then(response => {
                        console.log(response.data);
                        this.getPhonesFunc();
                    });

            },
            //Удаление кортежа из таблицы phones
            removePhoneFunc: function () {
                console.log(this.idremovePhone);
                axios.post('/Home/RemovePhone?id=' + this.idremovePhone)
                    .then(response => {
                        console.log(response.data);
                        this.getPhonesFunc();

                    });
                
                this.updateArea.visibility = false;
            },
            addAreaVisFunc: function () {
                this.addArea.visibility = true;
                this.updateArea.visibility = false;
            },
            editAreaVisFunc: function (phone) {
                this.getPhonesFunc();
                this.addArea.visibility = false;
                this.updateArea.visibility = true;
                this.editPhone = phone;


            },
            //Импорт коллекции phones из CSV файла
            importCSV: function () {
                var fileInput = document.getElementById('csvFile');
                var file = fileInput.files[0];
                const formData = new FormData();
                formData.append('file', file);
                axios.post('/Home/ImportPhonesFromCSV', formData)
                    .then(responce => {
                        console.log(responce.data);
                        this.getPhonesFunc();

                    });

            },
            openEditModal() {

                this.showEditModal = true;
            },
            closeEditModal() {
                this.showEditModal = false;
            },
            confirmEdit() {

                this.editPhoneFunc();
                this.closeEditModal();

            },
            cancelEdit() {
                // Действие после отмены
                this.closeEditModal();
            },
            openRemoveModal(phone) {
                this.idremovePhone = phone.id;
                this.showRemoveModal = true;
            },
            closeRemoveModal() {
                idremovePhone = 0;
                this.showRemoveModal = false;
            },
            confirmRemove() {

                this.removePhoneFunc();
                this.closeRemoveModal();

            },
            cancelRemove() {

                this.closeRemoveModal();
            }

        }
    });