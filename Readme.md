# GroceryCo

A sample implementation of a Kiosk to check out a basket full of items and apply discounts and promotions to get the best available deal.  
The application is developed using .NET core 3.1

## Notes

The Application takes in a text file, with each item purchased in a single line. Every item, has a PLU and a description separated by a pipe.  
Eg.  
2300 | Banana  
4211 | Apple  

## DataFiles  

The application looks for the following files  

* Products.txt - Contains the master list of all available products in the Store.
* SalePriceList.txt - Contains all the items offered on Sale.
* Promotions.txt - Contains all the promotions offered.

### Products.txt  

This the the master list of all available prouducts. Every line represents a product and is separated by a pipe.
The three columns represent the PLU, Descrption and Price.  
The price is considered decimal and will only consider the first to decimal places and ignore the rest.  
Eg.  
2141 | Apple | 3.22  
2101 | Orange | 2.79

### SalePriceListtxt  

This contains the items, that are up for sale. Every line represents a product with its PLU and sale price separated by a pipe.  
Eg.  
2141 | 1.60  
2101 | 0.99  

### Promotions.txt  

This contains the items, that are being promoted. Every promotion is defined in a single line and starts with a PromotionCode. This PromotionCode cannot be changed unless the underlying C# code is also informed about this change.
Currently only the following style of promotions are handled  

1. AdditionalProductDiscount - Buy X number of items and get Y number of items at a discount.  
2. GroupPromotionalPrice - Buy in groups of X at Y price.  

Promotion #1 (AdditionalProductDiscount)  
Every line is a unique promotion.

```CSV
AdditionalProductDiscount | 3291| 2 | 1 | 50  
```

The first column, is the description of this promotion and cannot be changed unless it is changed inside the code as well.
Second column specified the PLU of the item.  
Third column contains the number of items, that must be purchased before this offer is activated.  
The fourth column contains the number of items, that will be offered to the customer in this offer.  
The fifth column contains the Discount percentage of the offer.  

The above promotion can be read as....  
When 2 units of Item PLU#3291 is purchased then, the 3rd unit will be offered at 50%.  

Promotion #2 (GroupPromotionalPrice)
Every line is a unique promotion.

```CSV
GroupPromotionalPrice | 2141 | 4 | 0 | 1.75
```

The first column, is the description of this promotion and cannot be changed unless it is changed inside the code as well.  
Second column specified the PLU of the item.  
Third column contains the number of items, that must be purchased before this offer is activated.  
The fourth column is completely ignored.
The fifth column contains the new price of the item.  

## Adding new Promotion

If a new promotion needs to be added. Then ...  

1. Define the exact promotion CODE in StoreDomain.Promotions.OfferedPromotions.  
2. Define a new Class ( preferrably at StoreDomain.Promotions ) and implement StoreDomain.Promotions.IPromotionCalculator.
3. Inside StoreDomain.PromotionalPrice.Apply() add the new promotion into the switch case.  
4. Define the Promotion template inside Promotions.txt as shown above for the other two promotions.

## Design Choices

* A DataLayer exposed by a Repository pattern was chosen to provide data to the domain layer.  
* Domain Layer provided a cart to the actual client ( Console app in this case )  
* Dependency Injection was not chosen for this project primarily because, this Console App ( kiosk ), is not a portable kiosk that can be installed into any Grocery Store ( this was not in the scope ). If that were to be the cause, then, each Grocery Store can inject its data repository into the kiosk for it to Checkout and use the prices, sales and promotions.  
* For every new promotion defined , a new code deployment is needed and plugin system of promotion was not chosen. However, there are many ways to make the promotions more dynamic by means, of defining them as webservices or independent plugins ( DLLs ) and load them dynamically when needed.  

## Assumptions

* Every product has a PLU ( price look up )  
* Data defined in the DataFiles ( Products, Promotions and SalePriceList ) is consistent and does not contain duplicates. Usually, a database would be storing all this data and validate it.  
* Only one kind of an offer can be applied to any given item, either a Sale or a Promotion and the best price offer is chosen at checkout.  
