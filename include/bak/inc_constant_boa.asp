<%
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
'	Program By :		Danuchpong Savetsila / Email : danuch@yahoo.com
'	Objective :		Constant value for boa payment gate way
'	Date Created:		23 March 2002
'	Date Modified:		23 March 2002
'+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

'atsiam.com merchant id
CONST BOA_MERCHANT_ID = "008000218"

'boa transaction code
CONST BOA_TRANS_AUTHORIZE_CODE = "0100"
CONST BOA_TRANS_AUTHORIZE_RESPONSE_CODE = "0110"
CONST BOA_TRANS_REVERSAL_CODE = "0400"
CONST BOA_TRANS_REVERSAL_RESPONSE_CODE = "0410"
CONST BOA_TRANS_SETTLEMENT_CODE = "0500"
CONST BOA_TRANS_SETTLEMENT_RESPONSE_CODE = "0510"

'boa currency code
CONST BOA_CURRENCY_BAHT = "000900"
CONST BOA_CURRENCY_US = "000901"
CONST BOA_CURRENCY_YEN = "000902"
CONST BOA_CURRENCY_SG = "000903"
CONST BOA_CURRENCY_HK = "000904"
CONST BOA_CURRENCY_EU = "000905"

'boa credit card type
CONST BOA_CARD_VISA = "1"
CONST BOA_CARD_MASTER = "2"

'boa payment type
CONST BOA_PAYMENT_CREDIT = "2"
CONST BOA_PAYMENT_DEBIT = "3"
CONST BOA_PAYMENT_ASIA_WALLET = "4"

'boa settlement type
CONST BOA_SETTLEMENT_AUTO = "1"
CONST BOA_SETTLEMENT_MANUAL = "2"
CONST BOA_SETTLEMENT_ESETTLEMENT = "3"

'boa response code
CONST BOA_RESPONSE_APPROVED = "00"
CONST BOA_RESPONSE_DECLINED = "01"
CONST BOA_RESPONSE_EXPIRED = "02"
CONST BOA_RESPONSE_MERCHANT_ERROR = "03"
CONST BOA_RESPONSE_TRANSACTION_ERROR = "12"
CONST BOA_RESPONSE_AMOUNT_ERROR = "13"
CONST BOA_RESPONSE_CREDIT_ERROR = "14"
CONST BOA_RESPONSE_NO_ACTION = "21"
CONST BOA_RESPONSE_FORMAT_ERROR = "30"
CONST BOA_RESPONSE_NO_FUND = "51"
CONST BOA_RESPONSE_TIMEOUT = "55"
CONST BOA_RESPONSE_PIN_ERROR = "75"
CONST BOA_RESPONSE_PIN_ERROR_1 = "83"
CONST BOA_RESPONSE_CVV_ERROR = "84"
CONST BOA_RESPONSE_ISSUER_ERROR = "91"
CONST BOA_RESPONSE_AUTHENTICATION_ERROR = "95"
CONST BOA_RESPONSE_SYSTEM_ERROR = "99"
%>