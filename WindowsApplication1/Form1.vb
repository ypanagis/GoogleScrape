Imports OpenQA.Selenium
Imports OpenQA.Selenium.Firefox
Imports OpenQA.Selenium.Support.UI
Imports WatiN.Core


Public Class Form1
    Const Google = "http://www.google.com"
    Function runSelenium(ByVal paramStr As String, ByVal pagesNo As Integer)
        Console.WriteLine("Entering")
        Dim driver = New FirefoxDriver
        driver.Navigate.GoToUrl(Google)
        Dim googleSearchBox = driver.FindElementByName("q")
        googleSearchBox.SendKeys(paramStr)
        googleSearchBox.Submit()
        'driver.Manage.Timeouts.ImplicitlyWait(TimeSpan.FromSeconds(10))
        Dim wait As New WebDriverWait(driver, System.TimeSpan.FromSeconds(10.0))
        wait.Until(ExpectedConditions.ElementExists(By.Id("resultStats")))

        Dim resultsList As New List(Of Result)

        For resultPage As Integer = 1 To pagesNo
            Dim searchResults = driver.FindElementsByXPath("//*[@id='rso']//h3[@class='r']/a") 'returns all result headings

            For Each result As IWebElement In searchResults

                resultsList.Add(New Result(paramStr, result.GetAttribute("href")))

            Next
            driver.Manage.Timeouts.ImplicitlyWait(TimeSpan.FromSeconds(0.5))
            Dim nextPage As IWebElement
            nextPage = driver.FindElementByXPath("//a[@id='pnnext']")
            If Not nextPage Is Nothing Then
                nextPage.Click()
            Else 
                GoTo Quit
            End If


        Next resultPage
Quit:   driver.Quit()
        Return resultsList
    End Function

    Private Sub SearchButton_Click() Handles SearchButton.Click
        Dim resultsList = runSelenium(TextBox1.Text.Trim, Conversion.Int(CrawlPages.Value))
        DataGridView1.DataSource = resultsList
    End Sub

    Private Sub clrButton_Click(sender As Object, e As EventArgs) Handles clrButton.Click
        DataGridView1.DataSource = Nothing
        TextBox1.Text = Nothing
        CrawlPages.Value = 1
    End Sub
End Class
