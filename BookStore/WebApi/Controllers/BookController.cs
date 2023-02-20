using System.Net;
 using Microsoft.AspNetCore.Mvc;
 using System;
 using System.Collections.Generic;



namespace WebApi.AddControllers{
     
     [ApiController]
      [Route("[controller]s")]

     public class BookKController : ControllerBase
     {
        private static List<Book> BookList = new List<Book>()


        {
            new Book {
                Id = 1,
                Title = "Learn Startup",
                GenreId=1,
                PageCount=200,
                PublishDate= new DateTime(2001,06,12)
            },
             new Book {
                Id = 2,
                Title = "Herland",
                GenreId=2,
                PageCount=250,
                PublishDate= new DateTime(2010,05,23)
            },
            new Book {
                Id = 3,
                Title = "Dune",
                GenreId=2,
                PageCount=540,
                PublishDate= new DateTime(2001,12,21)
            },
        };

         [HttpGet]

         public List<Book> GetBooks()
         {
             var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
             return bookList;
         }

         [HttpGet ("{id}")] 
          // Route ile
        public Book GetById(int id )
        {
            var book = BookList.Where(x => x.Id == id ).SingleOrDefault();
            return book;
        }

         
        // query ile (sadece bir get kabul ediyor o nedenle yoruma aldım)
        //   [HttpGet] 
        // public Book Get([FromQuery] string id )
        // {
        //     var book2 = BookList.Where(x => x.Id ==  Convert.ToInt32(id)).SingleOrDefault();
        //     return book2;
        // }



       [HttpPost]

       public IActionResult AddBook ([FromBody] Book newBook )

       {
         var book = BookList.SingleOrDefault(x => x.Title == newBook.Title);

         if ( book != null )
            return BadRequest();

          BookList.Add(newBook);
            return Ok();    

       }

       [HttpPut("{id}")]

       public IActionResult UpdateBook(int id , [FromBody] Book updateBook)
       {
         var book = BookList.SingleOrDefault(x => x.Id == id);

         if  (book == null )
            return BadRequest();


         book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
         book.PageCount= updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
         book.PublishDate= updateBook.PublishDate != default ? updateBook.PublishDate: book.PublishDate;
         book.Title = updateBook.Title != default ? updateBook.Title : book.Title;

         return Ok();

         





       }
        

        [HttpDelete("{id}")]

         public IActionResult DeleteBook(int id)
         {
            var book = BookList.SingleOrDefault(x=> x.Id == id);
            if (book == null )
             return BadRequest();

             BookList.Remove(book);
             return Ok();
         }



  
     }


}