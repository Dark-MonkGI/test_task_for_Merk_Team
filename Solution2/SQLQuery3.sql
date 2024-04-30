--Запрос 3.
--Вывести список литературных произведений, которые встречаются и в сборниках и в отдельных изданиях. 
--Отсортировать по имени автора и году написания
--1. Title
--2. AuthorName
--3. YearOfWriting


SELECT 
    lw.Title,
    a.Name AS AuthorName,
    lw.YearOfWriting
FROM 
    LiteraryWorks lw INNER JOIN Authors a
                           ON lw.AuthorId = a.Id
WHERE 
    lw.Id IN (
				SELECT 
					lwb.LiteraryWorkId 
				FROM 
					LiteraryWorks_Books lwb
				GROUP BY 
					lwb.LiteraryWorkId 
				HAVING 
					COUNT(lwb.BookId) != 1
			 )
ORDER BY 
    AuthorName, YearOfWriting;