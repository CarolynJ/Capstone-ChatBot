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

INSERT INTO Resources  VALUES ('Preparing for a career fair', 'https://docs.google.com/document/d/1D_z7d0DTicboJF9O_U8okVS_ik9-EcDy_91ZpZRBugg/edit', 1);
INSERT INTO Resources  VALUES ('CBUS Foord/ Drink/ Space', 'https://drive.google.com/drive/folders/0B0rv0K0lgUzVLVQwU0ZSc3RPT0k', 1);
INSERT INTO Resources  VALUES ('Cover Letters & Follow up', 'https://drive.google.com/drive/folders/0B0L4DaCt03tFTHNkOThzMi1IckU', 1);
INSERT INTO Resources  VALUES ('Cover letter and Follow up Examples', 'https://drive.google.com/file/d/0B-Xlc61CFPaTdDVnbnB1bXZLWDFuTi1mQk45Z2tGX09rc2N3/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('2016 Cover Letter Tips', 'https://drive.google.com/file/d/0B0L4DaCt03tFZ0JaNl9hbE5IcFk/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Interview Follow Up Example', 'https://drive.google.com/file/d/0B0L4DaCt03tFYTFuVXJOU3RRSkE/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Power Words for Cover Letters', 'https://drive.google.com/file/d/0B0L4DaCt03tFS1ZmLUtobV9tWm8/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Elevator Pitch Worksheet 2017', 'https://drive.google.com/drive/folders/0B-Xlc61CFPaTZk94c1VfMXlZenM', 1);
INSERT INTO Resources  VALUES ('Interview Practice folder', 'https://drive.google.com/drive/folders/0B0rv0K0lgUzVTE0zb0w3dGgyMUE', 1);
INSERT INTO Resources  VALUES ('Behavioral interview practice: The STAR Model', 'https://docs.google.com/document/d/1V25ImgJDrxDH_CFDg9fIBjbU0AY53cqPTSqGN4mkSP4/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Best Tips for Acing a Phone Interview', 'https://drive.google.com/file/d/0B2j82Sfs0NxIZm03X2JOU2NILVU/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Interview Follow up Remineds/ Best Practices', 'https://docs.google.com/document/d/14OBiOmeOQRAcFRGcFrdY_NVuWVQVf0SxeoDyRDVo62E/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Interview Prep Email Templates', 'https://drive.google.com/file/d/0B0rv0K0lgUzVclJQMFJpejdvOHc/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Phone Interview Tips', 'https://docs.google.com/document/d/1iKKmaIAwcxXxx0F38h4k9YYTcoHA0cKaJ5RVJtugfvc/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Phone interviews (preperation)', 'https://drive.google.com/file/d/0B2j82Sfs0NxIZy1QOUpoWFBPa2c/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Matchmaking Best Practices & Reminders', 'https://docs.google.com/document/d/1zuCTGArPyvYpSAxtWlWlKR7ETRgTrq2_a-kRibI4PFE/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Sample Interview Questions folder', 'https://drive.google.com/drive/folders/0B0rv0K0lgUzVMHlyQjJIVF9sVkE', 1);
INSERT INTO Resources  VALUES ('30 Begavioral Interview Questions You Should be Ready to Answer', 'https://drive.google.com/file/d/0B2j82Sfs0NxIU2Zyc1NqRkt2bDQ/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('51 Interview Questions YOU should be Asking (word link)', 'https://docs.google.com/document/d/1aSIoYk9UjofSCqRv71OE7my9VEGKJe4ttPwrnuKa9d8/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('51 Interview Questions YOU should be Asking (web link)', 'https://www.themuse.com/advice/51-interview-questions-you-should-be-asking', 1);
INSERT INTO Resources  VALUES ('How to Answer the 31 Most Common Interview Questions(word link)', 'https://docs.google.com/document/d/1Xg1GRaQRJswgG6zp7deDKJtKlWeCqql0wxU_xWbXbIY/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('How to Answer the 31 Most Common Interview Questions(web link)', 'https://www.themuse.com/advice/how-to-answer-the-31-most-common-interview-questions', 1);
INSERT INTO Resources  VALUES ('Bahavioral Interview Questions', 'https://drive.google.com/file/d/0B0rv0K0lgUzVWHpoX3lCWllrU28/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Sample Interview Questions', 'https://drive.google.com/file/d/0B2-sQlN-4w_dRUx3YndMVnNKaGc/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Managing your job search', 'https://drive.google.com/drive/folders/0B0L4DaCt03tFOU5IVU8tQVZacE0', 1);
INSERT INTO Resources  VALUES ('Job Search Resources - list', 'https://drive.google.com/file/d/0B0L4DaCt03tFTnl1eUlRYjUwQTg/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Taking Ownership of Your Career', 'https://docs.google.com/document/d/1oHFwQkult09YuwCvMaPapDkorllaXlBGPHrKrF557U4/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Technology Team Roles Definitions', 'https://drive.google.com/file/d/0B-Xlc61CFPaTZkFzS0NvZHR5WE0/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('LinkedIn folder', 'https://drive.google.com/drive/folders/0B0rv0K0lgUzVTXZ3cUNzZndmU3M', 1);
INSERT INTO Resources  VALUES ('Get noticed with an amazing linkedIn profile', 'https://docs.google.com/document/d/1bQQ9jusneGeh0zJXjNnG5SgVhkSuyXit3cGsP3bm6n0/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('LinkedIn Update Steps: Add education + experience', 'https://docs.google.com/document/d/1lVFOYiCWYQs41wjUXerxeZGEMho7y-7KqZGjmIXSXvo/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('LinkedIn Updates: Checklist', 'https://docs.google.com/document/d/1xrGfKHZ1Hl8y2KStvnKTbCaq8XI_pw9Vi08GsdjDCA0/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Resume folder', 'https://drive.google.com/drive/folders/0B0rv0K0lgUzVN2xWZlJzTHdmODA', 1);
INSERT INTO Resources  VALUES ('Checklist for Approved Resume', 'https://docs.google.com/document/d/1Yxfcxq8rFhk_k_Z0oexn0Dpf6jArgLi3yT-HYckUsCU/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Resume Overview: Checklist', 'https://docs.google.com/document/d/1Em8-swvCIHBvAi3vdaOU76FGDL-q_lSuYyeYxuOmtVU/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Resume: List of Verbs to Consider', 'https://docs.google.com/document/d/1XmMVnoByfUvir86RQfts0R20shl3vBQ0bfz_aBfdPJo/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Sample Student Resume', 'https://docs.google.com/document/d/1Lq6SPRQeUw3ZEmLuOgC_bNBeKvMw7M_XOclHQL8XFqY/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Resume & LinkedIn Summary', 'https://docs.google.com/document/d/1ZCiJtSnw47BR_y46T55cFY_XoMUJ7rNi7Zmf0vbYZKg/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('StrengthsFinder Materials', 'https://drive.google.com/drive/folders/0B-Xlc61CFPaTNmlMQTM2U0YwRUE', 1);
INSERT INTO Resources  VALUES ('StrengthsFinder Descriptions', 'https://drive.google.com/file/d/0B2j82Sfs0NxIV01DWkhZaXdYMFk/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('StrengthsFinder: Points to Consider', 'https://docs.google.com/document/d/1bUL7gNLdNvcEIXRcDd_OOdb37auS2m6TDAJoEDKIkao/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Tips for Entry-Level Developers', 'https://drive.google.com/drive/folders/0B0rv0K0lgUzVRE5acldPVld2VlU', 1);
INSERT INTO Resources  VALUES ('Message from Google Software engineer to Entry-Level Developers', 'https://drive.google.com/file/d/0B-Xlc61CFPaTS1I3YTdVNlZzdHM4QWFpYmxpdnBPQkI4QkFV/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Message from Sr Female Developer to Jr Female Developers', 'https://docs.google.com/document/d/16HVm9CkgNnPkwmG0tR5jtBcjSNhz64VR2Dzm_TqJ3Co/edit?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Overcoming Impostor Syndrome', 'https://drive.google.com/file/d/0B0L4DaCt03tFRWFWc1dzZWpXbFk/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('Why Junior Developers are useful to companies', 'https://drive.google.com/file/d/0B0L4DaCt03tFZTVTNUFYd0VqUmc/view?usp=sharing', 1);
INSERT INTO Resources  VALUES ('CBUS Foord/ Drink/ Space', 'https://drive.google.com/drive/folders/0B0rv0K0lgUzVLVQwU0ZSc3RPT0k', 1);
INSERT INTO Resources  VALUES ('Matchmaking Best Practices & Reminders', 'https://docs.google.com/document/d/1zuCTGArPyvYpSAxtWlWlKR7ETRgTrq2_a-kRibI4PFE/edit?usp=sharing', 1);

-- order is important, if adding a new keyword please add to the end --
-- do not delete a keyword--

Insert into keywords values ('class');
insert into keywords values ('classes');
insert into keywords values ('abstract');
insert into keywords values ('encapsulation');
insert into keywords values ('inheritance');
insert into keywords values ('polymorphism');
insert into keywords values ('abstraction');
insert into keywords values ('int');
insert into keywords values ('string');
insert into keywords values ('tdd');
insert into keywords values ('test');
insert into keywords values ('testing');
insert into keywords values ('testable');
insert into keywords values ('unit test');
insert into keywords values ('regression');
insert into keywords values ('file');
insert into keywords values ('i/o');
insert into keywords values ('cli');
insert into keywords values ('stack');
insert into keywords values ('heap');
insert into keywords values ('method');
insert into keywords values ('overriding');
insert into keywords values ('virtual');
insert into keywords values ('overloading');
insert into keywords values ('clean');
insert into keywords values ('git');
insert into keywords values ('version');
insert into keywords values ('bash');
insert into keywords values ('bitbucket');
insert into keywords values ('github');
insert into keywords values ('visual');
insert into keywords values ('studio');
insert into keywords values ('ide');
insert into keywords values ('collection');
insert into keywords values ('array');
insert into keywords values ('list');
insert into keywords values ('dictionary');
insert into keywords values ('stack');
insert into keywords values ('queue');
insert into keywords values ('loop');
insert into keywords values ('foreach');
insert into keywords values ('for each');
insert into keywords values ('for');
insert into keywords values ('while');
insert into keywords values ('interface');
insert into keywords values ('definition');
insert into keywords values ('using');
insert into keywords values ('exception');
insert into keywords values ('expression');
insert into keywords values ('logic');
insert into keywords values ('if');
insert into keywords values ('else');
insert into keywords values ('=');
insert into keywords values ('>');
insert into keywords values ('>=');
insert into keywords values ('==');
insert into keywords values ('===');
insert into keywords values ('<');
insert into keywords values ('<=');
insert into keywords values ('c#');
insert into keywords values ('javascript');
insert into keywords values ('sql');
insert into keywords values ('server');
insert into keywords values ('mvc');
insert into keywords values ('mvc5');
insert into keywords values ('asp.net');
insert into keywords values ('.net');
insert into keywords values ('model');
insert into keywords values ('view');
insert into keywords values ('controller');
insert into keywords values ('jquery');
insert into keywords values ('debugging');
insert into keywords values ('debug');
insert into keywords values ('bug');
insert into keywords values ('api');
insert into keywords values ('json');
insert into keywords values ('rick');
insert into keywords values ('astley');
insert into keywords values ('feeling');
insert into keywords values ('gonna');
insert into keywords values ('never');
insert into keywords values ('give');
insert into keywords values ('database');
insert into keywords values ('join');
insert into keywords values ('inner');
insert into keywords values ('left');
insert into keywords values ('right');
insert into keywords values ('primary');
insert into keywords values ('foreign');
insert into keywords values ('key');
insert into keywords values ('dependency');
insert into keywords values ('ninject');
insert into keywords values ('connection');
insert into keywords values ('dal');
insert into keywords values ('css');
insert into keywords values ('html');
insert into keywords values ('tag');
insert into keywords values ('bootstrap');
insert into keywords values ('responsive');
insert into keywords values ('validation');    
insert into keywords values ('module 1');
insert into keywords values ('module one');
insert into keywords values ('object oriented programming');
insert into keywords values ('oop');
insert into keywords values ('fundamentals');
insert into keywords values ('module 2');
insert into keywords values ('module two');
insert into keywords values ('module 3');
insert into keywords values ('module three');
insert into keywords values ('module 4');
insert into keywords values ('module four');
insert into keywords values ('module 5');
insert into keywords values ('module five');

--  pathway keywords
insert into Keywords 
values ('cover letter'), ('letters'), ('follow up'), ('thank you'), ('notes'), ('power words'), ('action words'), ('interview follow up'),
('interview'), ('interviews'), ('questions'), ('behavioral'), ('phone'), ('reminder'), ('tips'), ('checklist'), ('strengthsfinder'), ('roles'),
 ('advice'), ('entry-level'), ('female developers'), ('women'), ('reume'), ('cv'), ('sample resume'), ('example'), ('example resume'), 
 ('profile'), ('linkedIn'), ('interviews'), ('career'), ('job search'), ('practice'), ('preparation'), ('developers'), ('jr'), ('verbs'), ('checklist'), 
 ('template'), ('elevator pitch'), ('elevator'), ('pitch'), ('food'), ('restaurants'), ('drink');

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
;

insert into Resource_Keyword values (1, 26), (1, 27), (2, 26), (2, 27), (3, 28), (3, 18), (4, 28), (4, 18);