-- SELECT * FROM Spins WHERE Spins.SessionId = "2" ORDER BY Spins.BetAmmount DESC
SELECT COUNT(Spins.SpinId), MAX(Spins.BetAmmount) as "Max Bet Ammount", Spins.SessionId FROM Spins GROUP BY Spins.SessionId