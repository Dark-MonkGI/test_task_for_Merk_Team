--Запрос 1.
--Вывести список авторов и число написанных произведений. Отсортировать по убыванию числа.
--Результирующие колонки:
--1. AuthorName
--2. Count

SELECT 
    a.Name AS AuthorName,
    COUNT(lw.Id) AS Count
FROM 
    Authors a INNER JOIN LiteraryWorks lw
                    ON a.Id = lw.AuthorId
GROUP BY 
    a.Name
ORDER BY 
    Count DESC;