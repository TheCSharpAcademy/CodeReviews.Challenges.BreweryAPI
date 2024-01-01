## Brewinpi

Little API for the [challenge project](https://www.thecsharpacademy.com/project/64) at the C# Academy.

These were the requisites

- List all beers by brewery
- A brewer can add, delete and update beers
- Add the sale of an existing beer to an existing wholesaler
- Upon a sale, the quantity of a beer needs to be incremented in the wholesaler's inventory
- A client can request a quote from a wholesaler.
- If successful, the quote returns a price and a summary of the quote. A 10% discount is applied for orders above 10 units. A 20% discount is applied for orders above 20 drinks.
- If there is an error, it returns an exception and a message to explain the reason: order cannot be empty; wholesaler must exist; there can't be any duplicates in the order; the number of beers ordered cannot be greater than the wholesaler's stock; the beer must be sold by the wholesaler
- A brewer brews one or several beers
- A beer is always linked to a brewer
- A beer can be sold by several wholesalers
- A wholesaler sells a defined list of beers, from any brewer, and has only a limited stock of those beers
- The beers sold by the wholesaler have a fixed price imposed by the brewery
- For this assessment, it is considered that all sales are made without tax
- The database is pre-filled by you
- No front-end is needed, just the API
- Use REST architecture
- Use Entity Framework
- No migrations are needed; use Ensure Deleted and Ensure Created to facilitate development and code reviews.

All requisites have been taken care off, the challenge requisites were also all completed. Those were:

- Add unit tests to make sure business constraints are accurate.
- Include a Read me with your thought process, your challenges and instructions on how to run the app.
- Add integrations tests using a real test database. These will ensure data is still added corrected when the codebase changes. The test database must be created and deleted for each test.
- Create a separate project with a front-end of your choice. Provide instructions on how to run it.

### The Api

#### BackEnd and BackEnd Tests

1. Clone the project, open the solution with visual studio and run the application.
2. You can also run the tests by right clicking the solution and choosing "run tests"

#### FrontEnd and FrontEnd Tests

1. Clone the project, go to *FrontEnd\BreweryAppAngular* and run "ng serve"
2. For the jasmine tests, go to the same folder and run "ng test" 


### Challenges and what was learned

This project was very fun to do!

I feel the back end was very straightforward to do but i definetly slowed down when i got to the front end part. Its interesting but i don't really like doing all the setup of pages and CSS.
That said it was refreshing re-learning angular with their new update that came out a few weeks ago, so taking this project at this time was probably beneficial. 
I will probably stick with angular for most thigns and just learn the necessary for react. 

Will still do projects with it but typescript and the way angular organizes things is really good, 
especially with the new update where you don't need to have the central file for all your imports and now everything is component based.

I tried to make the project use the repository pattern but i didn't really felt the need to add the service classes for the controllers at first, but when i was refactoring the tests and some of the controllers to have the functionality i need i realized why they are so necessary.
My controllers started getting bloated and was necessary to separate the functionality of validation and inserting.

There was also some difficulty regarding the new way .NET API projects work, the new way is using the program.cs file instead of the old startup.cs file, so a lot of ways to add middleware and how to make the system recognize your injections took some googling.

Figuring out how to organize the database took some time too but i feel i did a good job with it.
I also had to use DTO's to send especialized data to the API/frontEnd but i'm not sure they were used correctly or if my implementation is the best practice, definetly something that would probably begood to have someone more experienced to take a look at.

#### Tests

The main challenges i feel were actually writing tests? I feel that test documentation is something that is sorely lacking overall. Test documentantions and examples only do simple examples like "testing a calculator" 
and most courses or tuorials for the language gloss over that topic a LOT. I believe tests and knolowdge about tests are something that everyone looks for in projects and new hires but the material is really lacking on that front.

I did write tests to what i feel was encessary, but i'll definetly need to keep improving on that front for future projects.


#### Main takeaways

Overall i feel this project was a good first challenge to do and a way to put some of my C# knowlowdge to the test.
I definetly feel i improved but there's still somethings i would like to work on more like delegates, didn't use that a lot here. 
LINQ was very fun to use along with EF but i could definetly go deeper with both of these things.

I worked with angular before so most of it was familiar, the new stuff definetly made it a better experience to use than before.
Testing took a while to grasp, but when i understood the framework better it was interesting to write some tests. Definetly something i also need to work a lot on though.

Tahnks for reading!