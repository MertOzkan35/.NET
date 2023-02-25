using System.Net;
 using Microsoft.AspNetCore.Mvc;
 using System;
 using System.Collections.Generic;



namespace WebApi.AddControllers{
     
     [ApiController]
      [Route("[controller]s")]

     public class BookKController : ControllerBase
     {
        private readonly BookStoreDbContext _context;

        public BookKController (BookStoreDbContext context)
        {
          _context = context;

        }


      

         [HttpGet]

         public List<Book> GetBooks()
         {
             var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
             return bookList;
         }

         [HttpGet ("{id}")] 
          // Route ile
        public Book GetById(int id )
        {
            var book = _context.Books.Where(x => x.Id == id ).SingleOrDefault();
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
         var book = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);

         if ( book != null )
            return BadRequest();

          _context.Books.Add(newBook);
          _context.SaveChanges();
            return Ok();    

       }

       [HttpPut("{id}")]

       public IActionResult UpdateBook(int id , [FromBody] Book updateBook)
       {
         var book = _context.Books.SingleOrDefault(x => x.Id == id);

         if  (book == null )
            return BadRequest();


         book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
         book.PageCount= updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
         book.PublishDate= updateBook.PublishDate != default ? updateBook.PublishDate: book.PublishDate;
         book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
          
          _context.SaveChanges();
         return Ok();

         





       }
        

        [HttpDelete("{id}")]

         public IActionResult DeleteBook(int id)
         {
            var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            if (book == null )
             return BadRequest();

             _context.Books.Remove(book);
             _context.SaveChanges();
             return Ok();
         }



  
     }


}