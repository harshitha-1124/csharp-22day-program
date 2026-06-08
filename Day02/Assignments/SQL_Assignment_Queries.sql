USE CareBridgeDB;
SELECT
    p.FullName AS ProviderName,
    d.Name AS DepartmentName,
    COUNT(e.EncounterId) AS TotalEncounters,
    RANK() OVER (ORDER BY COUNT(e.EncounterId) DESC) AS ProviderRank
FROM Provider p
JOIN Department d 
    ON p.DepartmentId = d.DepartmentId
JOIN Encounter e 
    ON p.ProviderId = e.ProviderId
GROUP BY 
    p.ProviderId, 
    p.FullName, 
    d.Name
ORDER BY 
    TotalEncounters DESC;


	ALTER TABLE Insurance
ADD
    ValidFrom DATETIME2
        GENERATED ALWAYS AS ROW START
        DEFAULT SYSUTCDATETIME(),

    ValidTo DATETIME2
        GENERATED ALWAYS AS ROW END
        DEFAULT '9999-12-31 23:59:59.9999999',

    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo);

	ALTER TABLE Insurance
SET (
    SYSTEM_VERSIONING = ON
    (
        HISTORY_TABLE = dbo.Insurance_History
    )
);

UPDATE Insurance
SET Payer = 'HARSH'
WHERE PatientId = 1;

SELECT
    InsuranceId,
    Payer,
    PolicyNumber,
    ValidFrom,
    ValidTo
FROM Insurance
FOR SYSTEM_TIME ALL
WHERE PatientId = 1;

CREATE VIEW Claims_view AS
SELECT 
    ClaimId,
    BilledAmount,
    ReimbursedAmt,
    Status
FROM Claim;

CREATE PROCEDURE SP_CLAIMS
AS
BEGIN
    SELECT 
        Status,
        COUNT(*) AS TotalClaims,
        SUM(BilledAmount) AS TotalBilledAmount,
        SUM(ISNULL(ReimbursedAmt, 0)) AS TotalReimbursedAmount,
        SUM(BilledAmount - ISNULL(ReimbursedAmt, 0)) AS OutstandingAmount,

        RANK() OVER (
            ORDER BY 
            SUM(BilledAmount - ISNULL(ReimbursedAmt, 0)) DESC
        ) AS LossRank

    FROM Claims_view
    GROUP BY Status
    ORDER BY OutstandingAmount DESC;
END;

EXEC SP_CLAIMS;


CREATE PROCEDURE sp_ExecutiveDashboard
AS
BEGIN

    -- 1. Total Active Patients
    SELECT 
        COUNT(*) AS TotalActivePatients
    FROM Patient
    WHERE IsActive = 1;


    -- 2. Average Length of Stay
    SELECT 
        AVG(DATEDIFF(DAY, AdmitDate, DischargeDate)) AS AvgLengthOfStay
    FROM Encounter
    WHERE DischargeDate IS NOT NULL;


    -- 3. Top 5 Departments by Encounters
    SELECT TOP 5 
        d.Name AS DepartmentName,
        COUNT(*) AS TotalEncounters
    FROM Encounter e
    JOIN Department d 
        ON e.DepartmentId = d.DepartmentId
    GROUP BY d.Name
    ORDER BY TotalEncounters DESC;


    -- 4. Readmissions in last 30 days
    SELECT 
        PatientId,
        COUNT(*) AS ReadmissionCount
    FROM Encounter
    WHERE AdmitDate >= DATEADD(DAY, -30, GETDATE())
    GROUP BY PatientId
    HAVING COUNT(*) > 1;


    -- 5. Denied Claims
    SELECT 
        COUNT(*) AS DeniedClaims
    FROM Claim
    WHERE Status = 'Denied';


    -- 6. Highest Workload Provider
    SELECT TOP 1
        p.FullName,
        COUNT(*) AS TotalEncounters
    FROM Provider p
    JOIN Encounter e
        ON e.ProviderId = p.ProviderId
    GROUP BY p.ProviderId, p.FullName
    ORDER BY TotalEncounters DESC;

END;
EXEC sp_ExecutiveDashboard;


CREATE PROCEDURE sp_30_day_readmission
AS
BEGIN
    SELECT 
        PatientId,
        COUNT(*) AS ReadmissionCount
    FROM Encounter
    WHERE AdmitDate >= DATEADD(DAY, -30, GETDATE())
    GROUP BY PatientId
    HAVING COUNT(*) > 1;
END;


CREATE PROCEDURE sp_high_risk_patients
AS
BEGIN
    SELECT 
        p.PatientId,
        p.FullName,
        DATEDIFF(YEAR, p.DateOfBirth, GETDATE()) AS Age,
        COUNT(e.EncounterId) AS TotalEncounters
    FROM Patient p
    LEFT JOIN Encounter e
        ON p.PatientId = e.PatientId
    GROUP BY p.PatientId, p.FullName, p.DateOfBirth
    HAVING 
        DATEDIFF(YEAR, p.DateOfBirth, GETDATE()) > 60
        OR COUNT(e.EncounterId) >= 3;
END;

CREATE PROCEDURE sp_high_workload_provider
AS
BEGIN
    SELECT TOP 1
        p.FullName,
        COUNT(*) AS TotalEncounters
    FROM Provider p
    JOIN Encounter e
        ON e.ProviderId = p.ProviderId
    GROUP BY p.ProviderId, p.FullName
    ORDER BY TotalEncounters DESC;
END;

SELECT @@SERVERNAME;


CREATE VIEW vw_Clinical AS
SELECT 
    p.PatientId,
    p.FullName,
    e.EncounterId,
    e.EncounterType,
    d.Description AS Diagnosis
FROM Patient p
JOIN Encounter e ON p.PatientId = e.PatientId
JOIN Diagnosis d ON e.EncounterId = d.EncounterId;

CREATE VIEW vw_Billing AS
SELECT 
    c.ClaimId,
    c.Status,
    c.BilledAmount,
    c.ReimbursedAmt,
    (c.BilledAmount - ISNULL(c.ReimbursedAmt, 0)) AS OutstandingAmount
FROM Claim c;


CREATE VIEW vw_Analytics_DeId AS
SELECT 
    CASE 
        WHEN DATEDIFF(YEAR, DateOfBirth, GETDATE()) < 20 THEN '0-20'
        WHEN DATEDIFF(YEAR, DateOfBirth, GETDATE()) BETWEEN 20 AND 40 THEN '20-40'
        WHEN DATEDIFF(YEAR, DateOfBirth, GETDATE()) BETWEEN 41 AND 60 THEN '40-60'
        ELSE '60+'
    END AS AgeBand,
    e.EncounterType
FROM Patient p
JOIN Encounter e ON p.PatientId = e.PatientId;