# Linkar Demo Desktop App With-Automatic Classes

executable in https://kosday.com/resources/

This demo shows how a persistent client works with an automatic class and shows database information on a form.

# MainWindow 
This window gets mandatory information that allows LinkarClient login and logout. If we have already done the login you will have access to the other forms.

# Customers (FormCustomers) 
This form uses CLkCustomer and CLkCustomers classes. 
Select button calls the ClkICustomers selection method from the plural class.This class uses the "calculate" option in order to charge all the selected data
Yoy can write a Customer name or ID in the selection dialog.
When you set a record, buttons will be enabled to call CRUD operations (Create, Read, Update y Delete) located in ClkItem singular class.
 

Data dumped on classes is made through reflection record by record, this is slow. This use is not recommended if you wish to obtain a big amount of records at the same time.

# Automatic Classes
Exclusive for .NET &Mono

Some functions can accept and return values directly from a class. These classes must contain extra information in its properties to be able to link with the sent or received data. 

The data dump about the classes is made through reflection, record by record, this makes it take a little longer to process each record. For this reason its not recommended to obtain a big number of record at once.

This is achieved through the following metadata:

# MVProperty
This attribute indicates the Linkar client that he must trying to feed this field with the operations information. To link the feature with its internal value the following features are used 

 

· AttributeNumber: the number of the property attribute. Is compulsory specifying this property or DictionaryName. 

 

· DictionaryName: the name of the property dictionary. Is compulsory specifying this property or AttributeNumber 

 

· InternalType: indicates the data conversions that will be made in the client.None: default value, indicates that  type of conversion is not required by the client.  

 

· InternalDate: indicates that the value is saved as an internal date 

 

· FormatedDate: indicates that the value is saved as a formatted date. 

 

# MVCalculated
this attribute indicates the Linkar client that he must trying to feed this calculated field with the operations data. To link the property with its internal value the following properties are used: 

 

· DictionaryName: the name of the calculated dictionary. This property is mandatory. 

 

· InternalType: indicates the data conversions that are made in the client. 

 

· None:defualt value, indicates that a conversion type is not required by the client. 

 

· InternalDate: indicates that the value is saved as an internal date. 

 

· FormatedDate: indicates that the value is saved as a formatted date. 

 

# MVList
It's used on list type objects and indicates the Linkar client must go across or to collect its values or fill it. When situated in a main class is understood that it's a multivalue list, in the secondary class they will be treated as subvalues, from there to next they will be ignored. 
