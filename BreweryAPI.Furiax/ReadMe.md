<h1> Brewery API </h1>
<br>
<h3> About the app </h3>
I created this API as a solution to the following challenge:<br>
-List all beers by brewery<br>
-A brewer can add, delete and update beers<br>
-Add the sale of an existing beer to an existing wholesaler<br>
-Upon a sale, the quantity of a beer needs to be incremented in the wholesaler's inventory<br>
-A client can request a quote from a wholesaler.<br>
-If successful, the quote returns a price and a summary of the quote. A 10% discount is applied for orders above 10 units. A 20% discount is applied for orders above 20 drinks.<br>
-If there is an error, it returns an exception and a message to explain the reason<br>
-A brewer brews one or several beers<br>
-A beer is always linked to a brewer<br>
-A beer can be sold by several wholesalers<br>
-A wholesaler sells a defined list of beers, from any brewer, and has only a limited stock of those beers<br>
-The beers sold by the wholesaler have a fixed price imposed by the brewery<br>
<br>
For this assessment the database is pre-filled by you, no front-end is needed, just the API.<br>
Use REST architecture, use Entity Framework, no migrations are needed, use Ensure Deleted and Ensure Created.<br>
<h3>How to use</h3>
This API exists out of 3 parts(Beer,Sales,Quotes), each part has his uses for the diffrent users(Brewer,Wholesaler,Customer).<br>
Beer:<br>
The POST/PUT/DELETE methods are only ment for the Brewer. With these methods he can add, update or delete beers from the database.<br>
The diffrent GET methods can be used by all users. The normal GET method displays a list of all beers from the database while the GET/Beer{Id} only shows one specific beer and the GET/Beer/BeersByBrewer/{brewerId} only displays all beers from a specified brewer.<br>
Sales:<br>
Here the wholesaler puts up his sales by adding, adjusting or removing them to the database with the POST/PUT or DELETE methods.<br>
The GET method gives an overview of all products that are on sale at all the diffrent wholesalers.<br>
It's also possible to see one specific sale by using the GET/Sales/{id} method<br>
Quotes<br>
Quotes only has a single POST method that will be used by thecustomer<br>
Customers can send a orderlist, containing one or more diffrent beers and there quantity's, to there desired wholesaler.<br>
The POST method will then check if the selected wholesaler has the stock to fullfill this order. If so, a summuary with all products and total price is calculated and returned. Discounts are also applied when requirements are met. If the wholesaler doesn't have enough stock or if there is something wrong with the orderlist the customer gets a corresponding message back.<br>
