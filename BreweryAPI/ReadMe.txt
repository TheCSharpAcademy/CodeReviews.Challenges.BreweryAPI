The C# Academy

Project: BreweryAPI


How to use:
----------
For each of the tables existing in the database there are 5 endpoints in each controller that can be used:
-to get all records
-to get a specific record
-to create a record
-to update a record
-to delete a record

In the Beers controller there is an extra method/endpoint that can be used to get all beers by brewery.
The database schema can be consulted in the image "BreweryAPI schema" in the project folder.


Challenges/dificulties:
----------------------
Up until this point the databases i created all had 1 or 2 (max) tables, they were simple databases with simple table relathionships. But in this project more tables were required to comply with the requirements and relathions between them needed to be established, which was a good challenge in itself. And one that I wanted to try to overcome.

Building the API methods in the controller was a challenge too, since as a beginner i did know all the "syntax" that I could or should use to make it work. During the process, I got a few errors while testing the methods in swagger and I had to figure out what the problem was. Which sometimes could be related to how the table relations were built. But with perseverance and google i overcame this, and trough repetition (by building many methods) I can say that i feel alot more confident about building API's than i was before.

	The tests project:
	---------------------
	I encountered several difficulties here.
	For example, i had to figure out that while the tests were running (since they were running async) they would make changes
	to the database of other tests, ultimately changing the results of some tests. So, it was never consistent, a test might 
	pass one time, and then run the tests again and it would fail for no aparent reason... So, each test had its own separate
	database to avoid this issue.
	Because of the environmental variable (that would decide which data would be seeded into the database) and due to the fact that the tests run async i 		encounted another bug because at the beggining of the tests the variable was set to "test mode" and and the end of the test it was set back 
	to "normal mode". This caused at least one or two tests to fail because a test that finished running would change the variable to normal mode and another 		test in the middle of running might seed the "working data" into its database instead of the "test data" and it would fail. So at the ending of every test i 		had to remove every statement that set the environmental variable to "normal mode". In retrospect, i should probably have not used "test data" just simply a 		different database.

	The Maui project:
	-------------------
	I haven´t encounted any especially daunting challenges in this project. There was some stuff that i wanted to do that i was not able to figure out how to do, 	though. It had to do with using a "FindByName" method to find specific buttons or entries in the lists that were created given the itemsource i specified, in 	the end it would always return null, even when everything seemed to be correct, so i went for other aproach to achieve what i wanted to do.


Though Proccess:
----------------
Since I considered myself very much a beginner when it came to building API's and i didn't think i could do it all by myself at this point, i had to watch a few tutorials to help me understand how to construct everything.
So i ended up using a repository patterns where each table of the database had its own controller, interface and imlementation of the interface (repository file) to interact with the table. 
So basically, I ended up using the repository file to create all the EF CRUD methods, and then the API controllers would access the specific repository they needed to access the entity framework methods in order to interact with the database.

Since entity framework is being used, the models (that define each property and the relations between the tables) and the context were defined. And then DTOs were created for the API/controller methods to use them. IMapper was used to facilitate the proccess of moving data between models and DTO's
IMapper (which i never had used before) made it easier and saved some time to build the project but i also learned that using it its not universally agreed upon by all developers.

A seed file also was created with all the data that will be used to populate the database (if it is empty), in the correct context.
	
	The tests project:
	----------------------------
	For this project i decided to crate a test for every single method of every controller of the API. Also, each test would use its own database (in memory 		database) which was seeded with "test data". So everytime a test would run it set a variable in the "main" file of the API project as a way to signal that 		the data that should be seeded into the current database is the "test data" and not the "working data". 


	The Maui project:
	-------------------
	This project was basically building a frontend to consume the breweryAPI methods (though not all methods were necessary, they still exist at least).
	There is a user selection screen at the main page (brewery or wholesaler). Then a page to select specifically your brewery or wholesaler or to create a new 	entity and then it moves to the next page where the user can make changes to stuff related to his own brewery or wholesaler.
	