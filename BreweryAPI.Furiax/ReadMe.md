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
This API exists out of 3 parts(Beer,Sales,Quotes), each part has his uses for the diffrent users(Brewer,Wholesaler,Customer).<br><br>
<ins>Beer:</ins><br>
The POST/PUT/DELETE methods are only ment for the Brewer. With these methods he can add, update or delete beers from the database.<br>
The diffrent GET methods can be used by all users. The normal GET method displays a list of all beers from the database while the GET/Beer{Id} only shows one specific beer and the GET/Beer/BeersByBrewer/{brewerId} only displays all beers from a specified brewer.<br><br>
<ins>Sales:</ins><br>
Here the wholesaler puts up his sales by adding, adjusting or removing them to the database with the POST/PUT or DELETE methods.<br>
The GET method gives an overview of all products that are on sale at all the diffrent wholesalers.<br>
It's also possible to see one specific sale by using the GET/Sales/{id} method<br><br>
<ins>Quotes:</ins><br>
Quotes only has a single POST method that will be used by thecustomer<br>
Customers can send a orderlist, containing one or more diffrent beers and there quantity's, to there desired wholesaler.<br>
The POST method will then check if the selected wholesaler has the stock to fullfill this order. If so, a summuary with all products and total price is calculated and returned. Discounts are also applied when requirements are met. If the wholesaler doesn't have enough stock or if there is something wrong with the orderlist the customer gets a corresponding message back.<br>
<br>
<h3>How did I experience this challenge</h3>
This was only the second API I created, and this one was far more complex than the first one. So I learned alot of new stuff from it.<br>
I started this challenge by first thinking about how many and which tables I would need and then draw them out. I needed a model for beer, brewery, wholesaler, sale. Those were easy, the tables for orders and quotes were a bit more tricky as they contained relationships with multiple other models. I even had to adjust them a bit when I was actually working with them.<br>
Once my models were finished I started the context page so I could make a connection to my database. This also took some time to figure out since I never used Ensure Create/ Ensure Delete before. But again with some Googling and some chatGPT I got it up and running.<br>
Once my database was running it was time to seed it with data, again something I hadn't done before. So with some Googling I found out an example of putting my seed data into the context page. So I did, altough now I'm writing this I'm not so sure anymore this is the right place for it. <br>
Now that my database was running and filled with data is was time to work on the different CRUD commands. I first finished the Beer one and then added Sales and Quotes as last. The Beer and Sales API commands were pretty straight forward and I experienced little to no problems creating them. Quotes was a bit harder to implement since there were some more conditions to be accounted for. You had to check the stock of the wholesaler, get the prices to be displayed, the discounts to be calculated, and so on. But I managed. :)<br>
<br>
One of the extra challenges was to add Unit Tests to the project and create an front-end, but at the time I made the API I wasn't far enough in my progress to do these. The unit tests chapter is the next one coming up so perhaps I add these later on.<br>


