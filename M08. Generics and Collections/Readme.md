## Recording
[2020Q3](https://videoportal.epam.com/video/vbdGYlolO4BA1B4NaWqy)

[2021Q1](https://videoportal.epam.com/video/KOrM7kn3Er80yDgBJp1w)

## Material Content 
- *Generic types.*
- *Type constraints: reference type,  value type, new(),  type inference and etc.*
- *Enumeration:  IEnumerable, IEnumerator, IEnumerable(T) and IEnumerator(T) .*
- *IEqualityComparer and EqualityComparer.*
- *Block iterator yield.*
- *ICollection, IList and IDictionary<TKey,TValue> Interfaces.*
- *Lists, Queues, Stacks, Sets and Dictionaries.*

## Books & Useful References 
- [Programming C# 5.0. Ian Griffiths. O'Reilly Media. 2012.](http://shop.oreilly.com/product/0636920024064.do) 
   - *Chapter 4.* Generics. [Download Example Code](https://resources.oreilly.com/examples/0636920024064/blob/master/Ch04.zip) 
   - *Chapter 5.* Collections. [Download Example Code](https://resources.oreilly.com/examples/0636920024064/blob/master/Ch05.zip)
- [C# in Depth. Jon Skeet. Manning Publications Co. 2013](https://www.manning.com/books/c-sharp-in-depth-third-edition)
   - *Chapter 3.* [Parameterized typing with generics.](https://livebook.manning.com/#!/book/c-sharp-in-depth-third-edition/chapter-3/)
   - *Appendix B.* [Generic collections in .NET.](https://livebook.manning.com/#!/book/c-sharp-in-depth-third-edition/appendix-b/)
- [C# 6.0 in a Nutshell. Joseph Albahari, Ben Albahari. O'Reilly Media. 2015.](http://shop.oreilly.com/product/0636920040323.do)
   - *Chapter 7.* Collections. [Code Listings](http://www.albahari.com/nutshell/ch07.aspx)
- [C# 5.0 Unleashed. Bart De Smet. Sams Publishing. 2013](https://www.goodreads.com/book/show/16284093-c-5-0-unleashed)
   - *Chapter 15.* Generic Types and Methods.
   - *Chapter 16.* Collection Types.
- [CLR via C#. Jeffrey Richter. Microsoft Press. 2010](https://www.goodreads.com/book/show/7121415-clr-via-c)
   - *Chapter 12.* Generics.
- [Pro .NET Performance: Optimize Your C# Applications. Sasha Goldshtein.](http://www.apress.com/us/book/9781430244585)
   - *Chapter 5.* Collections and Generics
   
## Tasks
1. Create a class library
2. Implement a BinarySearch generic method.
3. Implement a method that returns IEnumerable<int> of the Fibonacci's numbers using the iterator block yield.
4. Develop a generic class-collection Queue/Stack that implements basic operations. Implement the capability to iterate by collection by design pattern Iterator.
5. Creata a console application which demonstrates the fucntionality of the class library.
6. Create another console application.
7. Implement a calculator which evaluates expressions in Reverse Polish notation. For example, expression 5 1 2 + 4 * + 3 - (which is equivalent to 5 + ((1 + 2) * 4) - 3 in normal notation) should evaluate to 14. Note, that for simplicity you may assume that there are always spaces between numbers and operations, e.g. 1 3 + expression is valid, but 1 3+ isn't. Empty expression should evaluate to 0. Valid operations are +, -, *, /.
