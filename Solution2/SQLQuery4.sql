--Запрос 4.
--Вывести отчет по популярности авторов у читателей: 
--автор, общее количество дней, когда его произведения были у читателей “на руках”, а также 3 самых популярных произведения
--- Популярность надо рассчитать по общему количеству дней, когда литературные произведения были у читателей “на руках”.
--- Для упрощения учитываются все экземпляры у всех читателей. Пересечения во
--времени не надо рассчитывать, т.е. даже если у одного и того же читателя
--одновременно находятся несколько экземпляров с одними и теми же
--произведениями - просто суммировать их
--- В сборниках время равномерно распределяется среди всех произведений.
--Например, если в сборнике 4 произведения, и сборник был у читателя 8 дней, то
--каждое произведение “получает” 2 дня.
--- Количество дней отсчитывается со следующего, т.е. если читатель вернул книгу в
--день получения - ничего не засчитываем
--- 3 самых популярных произведения склеиваются и выводятся в отдельной колонке
--одной строкой в кавычках и разделенные запятыми.

WITH CountDaysOnHand AS (
    SELECT 
        bl.BookCopyId, 
        lw.AuthorId, 
        lw.Id AS LiteraryWorkId, 
        lw.Title,
        (DATEDIFF(day, bl.LoanDate, bl.ReturnDate) / COUNT(lwb.LiteraryWorkId)) AS Days
    FROM 
        BookLoans bl INNER JOIN BookCopies bc 
							ON bl.BookCopyId = bc.Id
					INNER JOIN LiteraryWorks_Books lwb 
							ON bc.BookId = lwb.BookId
					INNER JOIN LiteraryWorks lw 
							ON lwb.LiteraryWorkId = lw.Id
    WHERE   
        bl.ReturnDate > bl.LoanDate
    GROUP BY 
        bl.BookCopyId, lw.AuthorId, lw.Id, lw.Title, bl.LoanDate, bl.ReturnDate
), 
-------------------------------
TotalCountDays AS (
    SELECT 
        AuthorId, 
        SUM(Days) AS TotalDays
    FROM 
        CountDaysOnHand 
    GROUP BY 
        AuthorId
), 
-------------------------------
TopWorks AS (
    SELECT 
        AuthorId, 
        Title,
        ROW_NUMBER() OVER (PARTITION BY AuthorId ORDER BY SUM(Days) DESC) AS rn
    FROM 
        CountDaysOnHand 
    GROUP BY 
        AuthorId, Title
)
-------------------------------
SELECT 
    a.Name AS AuthorName,
    tld.TotalDays AS CountAll ,
    TW1.Title + ', ' + TW2.Title + ', ' + TW3.Title AS Top3
FROM 
	Authors a INNER JOIN TotalCountDays tld 
					ON a.Id = tld.AuthorId
				LEFT JOIN TopWorks tw1 
					ON a.Id = tw1.AuthorId AND tw1.rn = 1
				LEFT JOIN TopWorks tw2 
					ON a.Id = tw2.AuthorId AND tw2.rn = 2
				LEFT JOIN TopWorks tw3 
					ON a.Id = tw3.AuthorId AND tw3.rn = 3
ORDER BY 
    TLD.TotalDays DESC;
