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