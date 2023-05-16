## Recording
[Module 9: Introduction to Language Integrated Query (LINQ)](https://videoportal.epam.com/video/EKz1JeNm0L9ylmzA7v5Q)

## Material Content 
- *LINQ-to-objects queries: Fluent Syntax and Query Operators.*
- *Lambda expressions and Func signatures. Query Expressions. Supporting Query Expressions. Deferred Evaluation. LINQ, Generics, and IQueryable(T).*
- *Standard LINQ Operators: Filtering, Select, SelectMany, Ordering, Specific Items and Subranges. Set Operations. Joins.*
- *Local Queries.*
- *Interpreted Queries (IQueryable preview).*
- *Combining Interpreted and Local Queries.*

## Books & Useful References 
- [Programming C# 5.0. Ian Griffiths. O'Reilly Media. 2012.](http://shop.oreilly.com/product/0636920024064.do) 
   - *Chapter 10.* LINQ. [Download Example Code](https://resources.oreilly.com/examples/0636920024064/blob/master/Ch10.zip) 
- [C# in Depth. Jon Skeet. Manning Publications Co. 2013](https://www.manning.com/books/c-sharp-in-depth-third-edition)
   - *Chapter 11.* [Query expressions and LINQ to Objects.](https://livebook.manning.com/#!/book/c-sharp-in-depth-third-edition/chapter-11/)
   - *Appendix A.* [LINQ standard query operators.](https://livebook.manning.com/#!/book/c-sharp-in-depth-third-edition/appendix-A/)
- [C# 6.0 in a Nutshell. Joseph Albahari, Ben Albahari. O'Reilly Media. 2015.](http://shop.oreilly.com/product/0636920040323.do)
   - *Chapter 8.* LINQ Queries. [Code Listings](http://www.albahari.com/nutshell/ch08.aspx)
   - *Chapter 9.* LINQ Operators. [Code Listings](http://www.albahari.com/nutshell/ch09.aspx)
- [C# 5.0 Unleashed. Bart De Smet. Sams Publishing. 2013](https://www.goodreads.com/book/show/16284093-c-5-0-unleashed)
   - *Chapter 19.* Language Integrated Query Essentials.
   - *Chapter 20.* Language Integrated Query Internals
- [101 LINQ Samples](https://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b)

## Tasks  
**Objective:**  
1. Develop a console application that uses LINQ queries to retrieve data. Data is stored in a .json file and represents information about the results of students tests.
2. Information about the student contains the name of the student, the name of the test, the date of the test and the assessment of the test for this student.
3. When the applications is being initialized, all the data from .json file should be loaded into memory. 
4. Provide ability to input search criteria using a search string as an input parameter (based on different flags).

Example:
Hello! Please input your criteria:
-name Ivan -minmark 3 -maxmark 5 -datefrom 20/11/2012 -dateto 20/12/2012 -test Maths

Result:
- Student        Test     Date        Mark
- Ivan Petrov    Maths    25/11/2012  4
- Ivan Ivanov    Maths    30/11/2012  3

5. Provide the ability to sort the result using -sort flag with two paramaters (for ex, -sort name asc)
6. Develop Unit Tests