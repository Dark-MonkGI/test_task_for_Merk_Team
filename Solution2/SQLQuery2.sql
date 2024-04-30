SELECT 
    r.LastName,
    r.FirstName,
    COUNT(DISTINCT lwb.LiteraryWorkId) AS Count
FROM 
	Readers r INNER JOIN BookLoans bl
					ON r.Id = bl.ReaderId
			  INNER JOIN BookCopies bc
					ON bl.BookCopyId = bc.Id
			  INNER JOIN LiteraryWorks_Books lwb
					ON bc.BookId = lwb.BookId
GROUP BY 
    r.LastName, r.FirstName
ORDER BY 
    Count DESC;