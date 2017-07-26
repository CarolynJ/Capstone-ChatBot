drop table resources;

create Table resources
(
	id int identity(1,1),
	resourceTitle varchar(100) not null,
	resourceLink varchar(300), 
	resourcePdf varchar(max)

);

INSERT INTO resources (resourceTitle, resourceLink) VALUES ('The Git Parable', 'http://tom.preston-werner.com/2009/05/19/the-git-parable.html');
KEYWORDS Git Module1 Bash Reading 'Version Control'


INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Git Cheat Sheet', 'https://drive.google.com/drive/folders/0Bz4DHj0l-C66QjRfN05LWWZIRGs');
KEYWORDS Git Module1 cheatsheet cheat sheet PDF

INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Navigating the shell', 'http://linuxcommand.org/lc3_lts0020.php');
KEYWORDS Module1 Linux Shell Command Line

INSERT INTO resources (resourceTitle, resourceLink) VALUES ('What is the shell?', 'http://linuxcommand.org/lc3_lts0010.php');
KEYWORDS Module1 Linux Shell Command Line


INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Command Line Cheat Sheet', 'https://drive.google.com/file/d/0Bz4DHj0l-C66ak9ZZVc0cjNZZU0/view');
KEYWORDS Module1 Linux Shell Command Line Cheat Sheet 


INSERT INTO resources (resourceTitle, resourceLink) VALUES ('C# Book', 'https://static1.squarespace.com/static/5019271be4b0807297e8f404/t/5824ad58f7e0ab31fc216843/1478798685347/CSharp+Book+2016+Rob+Miles+8.2.pdf
');
KEYWORDS Module1 C# Cheese Book 

INSERT INTO resources (resourceTitle, resourceLink) VALUES ('C# Data Types', 'https://drive.google.com/file/d/0Bz4DHj0l-C66eVpPa0RBbk5TUlU/view');
KEYWORDS Module1 C# Cheese Book 

INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Visual Studio Cheat Sheet', 'https://drive.google.com/file/d/0Bz4DHj0l-C66bGF1ejZvMndOVmc/view');
KEYWORDS Module1 'Visual Studio' 'Cheat Sheet'

INSERT INTO resources (resourceTitle, resourceLink) VALUES ('If-else reference', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/if-else#');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Arrays Tutorial', 'https://msdn.microsoft.com/en-us/library/aa288453(v=vs.71).aspx');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Channel9 Debugging 101 in Visual Studio', 'https://channel9.msdn.com/Blogs/Seth-Juarez/Debugging-101-in-Visual-Studio-with-Andrew-Hall');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('String in C# and .Net', 'http://csharpindepth.com/Articles/General/Strings.aspx');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('String Cheat Sheet', 'https://docs.google.com/spreadsheets/d/1WgFw2jjVcxc-afK840jvDHo9zyAQ9dCizzsMNi0T_qQ/edit?usp=drive_web');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('String Programming Guide', 'https://docs.google.com/spreadsheets/d/1WgFw2jjVcxc-afK840jvDHo9zyAQ9dCizzsMNi0T_qQ/edit?usp=drive_web');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Date Time Reference', 'https://msdn.microsoft.com/en-us/library/system.datetime(v=vs.110).aspx');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Math Library Reference', 'https://msdn.microsoft.com/en-us/library/system.math(v=vs.110).aspx');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('C# Using Keyword', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('For-each Reference', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/foreach-in');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('C# Enum Data type', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/enu');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('6 Important Concepts About Stack & Heap', 'https://www.codeproject.com/Articles/76153/Six-important-NET-concepts-Stack-heap-value-types');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Channel 9 MSDN Debugging 101', 'https://channel9.msdn.com/Blogs/Seth-Juarez/Debugging-101-in-Visual-Studio-with-Andrew-Hall');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Properties C# Programming Guide', 'https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/using-properties');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Read/Write Properties', 'https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-declare-and-use-read-write-properties');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('How to Write Testable Code & Why it Matters', 'https://www.toptal.com/qa/how-to-write-testable-code-and-why-it-matters');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Using Assert Classes', 'https://msdn.microsoft.com/en-us/library/ms182530.aspx');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('C# virtual keyword', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Casting Safely with as and is', 'https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-safely-cast-by-using-as-and-is-operators');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('C# Abstract Classes', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Overriding Equals', 'https://msdn.microsoft.com/en-us/library/bsc2ak47.aspx');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('C# Abstract Classes', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('C# class Keyword', 'https://drive.google.com/file/d/0Bz4DHj0l-C66OWFxNmtPaFV3dEU/view');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Test Driven Development', 'http://www.jamesshore.com/Agile-Book/test_driven_development.html');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Refactoring Techniques', 'https://sourcemaking.com/refactoring');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Clean Code book on Amazon', 'https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Exception Handling Guide', 'https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/exceptions/exception-handling');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('How to Common i/o tasks', 'https://docs.microsoft.com/en-us/dotnet/standard/io/common-i-o-tasks');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('Best Practices For Exception Handling', 'https://docs.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions');

INSERT INTO resources (resourceTitle, resourceLink) VALUES ('', '');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('', '');
INSERT INTO resources (resourceTitle, resourceLink) VALUES ('', '');
