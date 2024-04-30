--«апрос 2.
--ѕолучить список читателей с количеством прочитанных литературных произведений.
--—читаем, что читатели читают все произведени€ в книгах, которые они брали.
--ѕеречитывание одного и того же произведени€ не считаетс€. —ортировка от самого читающего к самому нечитающему. 
--–езультирующие колонки:
--1. LastName
--2. FirstName
--3. Count


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