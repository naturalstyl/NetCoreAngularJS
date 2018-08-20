(function () {
    'use strict';

    angular
        .module('app')
        .factory('ledgerServices', ledgerServices);

    ledgerServices.$inject = ['$http','$resource'];

    function ledgerServices($http, $resource) {
        var service = {
            getData: getData,
            requestPayment:requestPayment
        };

        return service;

        function getData(accountId, ledgerStartDate) {
            return $http.get('https://localhost:44374/api/ledger/' + accountId + '?startDate=' + ledgerStartDate);
        }

        function requestPayment(paymentRequest) {
            return $http.post('https://localhost:44374/api/ledger', paymentRequest);
        }
    }
})();