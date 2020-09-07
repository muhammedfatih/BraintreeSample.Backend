# Braintree Sample Backend Project

Project aims to demonstrate sample usage of Braintree API. Project contains swagger. 

## 1. API Endpoints

* POST /api/user/

* POST /api/user/login

* POST /api/creditcard/tokenize

* DELETE /api/creditcard/{guid}

* GET /api/creditcard/

* POST /api/transaction/sale

* GET /api/transaction/

## 2. Authentication

In order to use API, basic authentication should be sent as reuqest header. Due to the security reasons, a simple token mechanism is used. First, token should be taken via login endpoint. Secondly that token should be used as password for basic authentication. 

## 3. Resources

* Test Credit Cards https://developers.braintreepayments.com/guides/credit-cards/testing-go-live/php

* Customer.Create() https://developers.braintreepayments.com/reference/request/customer/create/dotnet

* CreditCard Object https://developers.braintreepayments.com/reference/response/credit-card/dotnet

* Transaction.Sale() https://developers.braintreepayments.com/reference/request/transaction/sale/dotnet

* Payment Errors https://developers.braintreepayments.com/reference/request/payment-method/create/dotnet