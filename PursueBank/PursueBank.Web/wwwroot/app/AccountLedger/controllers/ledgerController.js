(function () {
    'use strict';

    angular
        .module('app')
        .controller('ledgerController', ledgerController);

    ledgerController.$inject = ['$location', '$window', 'ledgerServices'];

    function ledgerController($location,$window, ledgerServices) {
        /* jshint validthis:true */
        var vm = this;
        var accountId = 1;
        vm.title = 'ledgerController';
        vm.acctTransactions = [];
        vm.paymentAmount = null;
        vm.transactionDate = '';
        vm.hasTransaction = false;
        vm.transactionDescription = '';
        const ledgerStartDate = '08-01-2018';

        vm.submitTransaction = function (form) {
            if (form.$valid) {
                if (form.$dirty) {
                    var transRequest = {
                        accountId: accountId,
                        sendByDate: vm.transactionDate,
                        amount: vm.paymentAmount,
                        description: vm.transactionDescription
                    };
                    requestPayment(transRequest);
                    form.$setUntouched();
                    form.$setPristine();
                }
            }
        };

        vm.clearForm = function () {
            vm.paymentAmount = null;
            vm.transactionDate = '';
            vm.transactionDescription = '';
        };

        function requestPayment(transRequest) {
          return  ledgerServices.requestPayment(transRequest)
              .then(function (response, status, headers, config) {
                    getAccountTransactions();
                },
                function (err, status, headers, config) {
                    alert("Error. Could not complete payment process.\n" + (err.data.Message || err.data.Data || ""));
                });
        }

        function getAccountTransactions() {
            ledgerServices.getData(accountId, ledgerStartDate)
                .then(function (response, status, headers, config) {
                    vm.acctTransactions = response.data;
                    vm.clearForm();
                },
                    function (err, status, headers, config) {
                        vm.acctTransactions = [];
                    }
                );
        }

        function setTransactionDateControlLimit() {
            var dateCtl = $('#transDate');
            var curDate = new Date();
            
            dateCtl.attr('min', (1+curDate.getMonth()) + '/' + curDate.getDate() + '/' + curDate.getFullYear());
        }

        activate();

        function activate() {
            setTransactionDateControlLimit();
            // Get initial data
            getAccountTransactions();
        }
    }
})();
