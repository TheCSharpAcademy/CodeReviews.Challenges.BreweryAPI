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
