drop table Resources;
drop table Motivation;
drop table Keywords;
drop table Resource_Keyword;

create Table Resources
(
	ResourceId int identity(1,1),
	ResourceTitle varchar(100) not null,
	ResourceContent varchar(max), 
	PathwayResource bit not null

	constraint pk_resources primary key(ResourceId)
);

create table Keywords
(
KeywordId int identity (1,1),
Keyword varchar(100) not null

constraint pk_keywords primary key (KeywordId)
);

create table Resource_Keyword
(
ResourceId int not null,
KeywordId int not null,

constraint pk_resourcekeyword primary key (ResourceId, KeywordId),
constraint fk_resourcekeyword_resource foreign key (ResourceId) References Resources(ResourceID),
constraint fk_resourcekeyword_keyword foreign key (KeywordId) References Keywords(KeywordId),

)

Create Table Motivation
(
	MotivationCode int identity(1,1) not null,
	Quote varchar(1000) not null,
	QuoteSource varchar(100) not null,
	ImageCode varchar (100) not null,

	constraint pk_motivation primary key (MotivationCode)
);

--order is important, if adding a resource please add to the end--
--do not delete a resource--

INSERT INTO Resources  VALUES ('The Git Parable', 'http://tom.preston-werner.com/2009/05/19/the-git-parable.html', 0);
INSERT INTO Resources  VALUES ('Git Cheat Sheet', 'https://drive.google.com/drive/folders/0Bz4DHj0l-C66QjRfN05LWWZIRGs', 0);
INSERT INTO Resources  VALUES ('Navigating the shell', 'http://linuxcommand.org/lc3_lts0020.php', 0);
INSERT INTO Resources  VALUES ('What is the shell?', 'http://linuxcommand.org/lc3_lts0010.php', 0);
INSERT INTO Resources  VALUES ('Command Line Cheat Sheet', 'https://drive.google.com/file/d/0Bz4DHj0l-C66ak9ZZVc0cjNZZU0/view', 0);
INSERT INTO Resources  VALUES ('C# Book', 'https://static1.squarespace.com/static/5019271be4b0807297e8f404/t/5824ad58f7e0ab31fc216843/1478798685347/CSharp+Book+2016+Rob+Miles+8.2.pdf
', 0);
INSERT INTO Resources  VALUES ('C# Data Types', 'https://drive.google.com/file/d/0Bz4DHj0l-C66eVpPa0RBbk5TUlU/view', 0);
INSERT INTO Resources  VALUES ('Visual Studio Cheat Sheet', 'https://drive.google.com/file/d/0Bz4DHj0l-C66bGF1ejZvMndOVmc/view', 0);
INSERT INTO Resources  VALUES ('If-else reference', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/if-else#', 0);
INSERT INTO Resources  VALUES ('Arrays Tutorial', 'https://msdn.microsoft.com/en-us/library/aa288453(v=vs.71).aspx', 0);
INSERT INTO Resources  VALUES ('Channel9 Debugging 101 in Visual Studio', 'https://channel9.msdn.com/Blogs/Seth-Juarez/Debugging-101-in-Visual-Studio-with-Andrew-Hall', 0);
INSERT INTO Resources  VALUES ('String in C# and .Net', 'http://csharpindepth.com/Articles/General/Strings.aspx', 0);
INSERT INTO Resources  VALUES ('String Cheat Sheet', 'https://docs.google.com/spreadsheets/d/1WgFw2jjVcxc-afK840jvDHo9zyAQ9dCizzsMNi0T_qQ/edit?usp=drive_web', 0);
INSERT INTO Resources  VALUES ('String Programming Guide', 'https://docs.google.com/spreadsheets/d/1WgFw2jjVcxc-afK840jvDHo9zyAQ9dCizzsMNi0T_qQ/edit?usp=drive_web', 0);
INSERT INTO Resources  VALUES ('Date Time Reference', 'https://msdn.microsoft.com/en-us/library/system.datetime(v=vs.110).aspx', 0);
INSERT INTO Resources  VALUES ('Math Library Reference', 'https://msdn.microsoft.com/en-us/library/system.math(v=vs.110).aspx', 0);
INSERT INTO Resources  VALUES ('C# Using Keyword', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive', 0);
INSERT INTO Resources  VALUES ('For-each Reference', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/foreach-in', 0);
INSERT INTO Resources  VALUES ('C# Enum Data type', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/enu', 0);
INSERT INTO Resources  VALUES ('6 Important Concepts About Stack & Heap', 'https://www.codeproject.com/Articles/76153/Six-important-NET-concepts-Stack-heap-value-types', 0);
INSERT INTO Resources  VALUES ('Channel 9 MSDN Debugging 101', 'https://channel9.msdn.com/Blogs/Seth-Juarez/Debugging-101-in-Visual-Studio-with-Andrew-Hall', 0);
INSERT INTO Resources  VALUES ('Properties C# Programming Guide', 'https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/using-properties', 0);
INSERT INTO Resources  VALUES ('Read/Write Properties', 'https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/how-to-declare-and-use-read-write-properties', 0);
INSERT INTO Resources  VALUES ('How to Write Testable Code & Why it Matters', 'https://www.toptal.com/qa/how-to-write-testable-code-and-why-it-matters', 0);
INSERT INTO Resources  VALUES ('Using Assert Classes', 'https://msdn.microsoft.com/en-us/library/ms182530.aspx', 0);
INSERT INTO Resources  VALUES ('C# virtual keyword', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual', 0);
INSERT INTO Resources  VALUES ('Casting Safely with as and is', 'https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-safely-cast-by-using-as-and-is-operators', 0);
INSERT INTO Resources  VALUES ('C# Abstract Classes', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract', 0);
INSERT INTO Resources  VALUES ('Overriding Equals', 'https://msdn.microsoft.com/en-us/library/bsc2ak47.aspx', 0);
INSERT INTO Resources  VALUES ('C# Abstract Classes', 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract', 0);
INSERT INTO Resources  VALUES ('C# class Keyword', 'https://drive.google.com/file/d/0Bz4DHj0l-C66OWFxNmtPaFV3dEU/view', 0);
INSERT INTO Resources  VALUES ('Test Driven Development', 'http://www.jamesshore.com/Agile-Book/test_driven_development.html', 0);
INSERT INTO Resources  VALUES ('Refactoring Techniques', 'https://sourcemaking.com/refactoring', 0);
INSERT INTO Resources  VALUES ('Clean Code book on Amazon', 'https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882', 0);
INSERT INTO Resources  VALUES ('Exception Handling Guide', 'https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/exceptions/exception-handling', 0);
INSERT INTO Resources  VALUES ('How to Common i/o tasks', 'https://docs.microsoft.com/en-us/dotnet/standard/io/common-i-o-tasks', 0);
INSERT INTO Resources  VALUES ('Best Practices For Exception Handling', 'https://docs.microsoft.com/en-us/dotnet/standard/exceptions/best-practices-for-exceptions', 0);
INSERT INTO Resources  VALUES ('Best Practices Tutorial', 'https://www.youtube.com/watch?v=dQw4w9WgXcQ', 0);
INSERT INTO Resources  VALUES ('Ninject', 'http://www.ninject.org/', 0);

-- order is important, if adding a new keyword please add to the end --
-- do not delete a keyword--

Insert into keywords values ('class', 'classes', 'abstract', 'encapsulation', 'inheritance', 'polymorphism', 'abstraction', 'int', 'string', 'tdd', 'test', 'testing', 'testable', 'unit test', 'regression', 'file', 'i/o', 'cli', 'stack', 'heap', 'method', 'overriding', 'virtual', 'overloading', 'clean', 
'git', 'version', 'bash', 'bitbucket', 'github', 'visual', 'studio', 'ide', 'collection', 'array', 'list', 'dictionary', 'stack', 'queue', 'loop', 'foreach', 'for each', 'for', 'while', 'interface', 'definition', 'using', 'exception', 'expression', 'logic', 'if', 'else', '=', '>', '>=', '==', '===', '<', '<=', 'c#', 'javascript', 
'sql', 'server', 'mvc', 'mvc5', 'asp.net', '.net', 'model', 'view', 'controller', 'jquery', 'debugging', 'debug', 'bug', 'api', 'json', 'rick', 'astley', 'feeling', 'gonna', 'never', 'give', 'database', 'join', 'inner', 'left', 'right', 'primary', 'foreign', 'key', 'dependency', 'ninject', 'connection', 'dal', 'css', 'html',
'tag', 'bootstrap', 'responsive', 'validation');    



insert into Motivation 
values ('Leadership is not about a title or a designation. It is about impact, influence and inspiration. Impact involves getting results, influence is about spreading the passion you have for your work, and you have to inspire team-mates and customers.', 'Robin S. Sharma', 'RobinSharma.png'),
('Inspiration is one thing and you cannot control it, but hard work is what keeps the ship moving. Good luck means, work hard. Keep up the good work.', 'Kevin Eubanks', 'kevineubanks.png'),
('I no have education. I have inspiration. If I was educated, I would be a damn fool.', 'Bob Marley', 'bobmarley.png'),
('Do not quench your inspiration and your imagination; do not become the slave of your model.', 'Vincent Van Gogh', 'vincentvangogh.png'),
('Expectations are a form of first-class truth: If people believe it, it''s true.', 'Bill Gates', 'billgates.png'),
('Don''t compare yourself with anyone in this world...If you do so, you are insulting yourself', 'Bill Gates', 'billgates.png'),
('Your time is limited, so don''t waste it living someone else''s life. Don''t be trapped by dogma - which is living with the results of other people''s thinking. Don''t let the noise of others'' opinions drown out your own inner voice. And most important, have the courage to follow your heart and intuition.', 'Steve Jobs', 'stevejobs.png'),
('Remembering that I''ll be dead soon is the most important tool I''ve ever encountered to help me make the big choices in life. Because almost everything - all external expectations, all pride, all fear of embarrassment or failure - these things just fall away in the face of death, leaving only what is truly important.', 'Steve Jobs', 'stevejobs.png'),
('I work very hard, and I play very hard. I''m grateful for life. And I live it - I believe life loves the liver of it. I live it.', 'Maya Angelou', 'mayaangelou.png'),
('I was a very shy girl who led an insulated life; it was only when I came to Oxford, and to Harvard before that, that suddenly I saw the power of people. I didn''t know such a power existed, I saw people criticising their own president; you couldn''t do that in Pakistan - you''d be thrown in prison.', 'Benazir Bhutto', 'benazirbhutto.png'),
('If it''s a good idea, go ahead and do it. It''s much easier to apologize than it is to get permission.', 'Grace Hopper', 'gracehopper.png'),
('I always did something I was a little not ready to do. I think that''s how you grow. When there''s that moment of ''Wow, I''m not really sure I can do this,'' and you push through those moments, that''s when you have a breakthrough.', 'Marissa Mayer', 'marissamayer.png'),
('I believe in simplicity.  I strive for simple code and straightforward explanations, and while I won''t claim I always succeed, my work is improved by the humble attempt.  The fact that yesterday''s ideas seem imperfect today gives me confidence that I''m still learning.', 'Sandi Metz', 'sandimetz.png')
