## Flapp Documentatie

### Connectiestring instellen

1. Open [\*SMSS](## 'SQL Server Management Studio').

2. Open **SQL Server Object Explorer**. (View -> SQL Server Object Explorer)
   Rechts klik op _SQL Server_ als je nog geen connectie hebt.

   ![connStr1](Images/connStr1.png)

3. Duid in local een server aan met _SQLEXPRESS_ en hier na duid je ook de database aan.

   ![connStr2](Images/connStr2.png)

4. Nu is de _SQL Server_ zichtbaar. Klik ze open en zoek de database, druk daarna rechts voor **Properties**.

   ![connStr3](Images/connStr3.png)

5. Een properties zijbalk zal openen met de connection string, kopieer het.

   ![connStr4](Images/connStr4.png)

6. Open het project in [\*VS](## 'Visual Studio'), in de class library is er een file genaamd `App.config`. <br>Hier in voeg je:
   `<add name="[NAAM]" connectionString="[CONNECTIESTRING]"/>` tussen `<connectionStrings>` en `</connectionStrings>` waar je _NAAM_ en _CONNECTIESTRING_ aanpast.

   ![connStr5](Images/connStr5.png)
