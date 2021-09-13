You should implement an application that:
1.	Downloads historical market daily data (date, open, close, high, low) from yahoo finance (or any other free/trial marked data service you like) for selected stocks, to a selected depth.
2.	Aggregates daily data with selected step (for example to weekly instead of daily)
3.	Export/save aggregated data to pdf document to the selected folder.
An application should be implemented with .Net 5 and C#
An application should contain UI to choose stocks which history it should download, select history depth and aggregation step/option, and select folder to save/export result.
You can implement UI any way you like such as Windows Forms, WPF or ASP.Net page.
An application should be ready to compile and run without any additional settings for solution or application from our side.

P.S. Yahoo Finance API is deprecated but you still can use it.
* This is an API url: https://query1.finance.yahoo.com/v7/finance
* This request: https://query1.finance.yahoo.com/v7/finance/download/AAPL?period1=1492524105&period2=1495116105&interval=1d&events=history&crumb=tO1hNZoUQeQ will return csv data
* This request https://query1.finance.yahoo.com/v7/finance/chart/AAPL?range=2y&interval=1d&indicators=quote&includeTimestamps=true will return json

