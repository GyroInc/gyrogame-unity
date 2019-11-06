package cucumber_demo;

import cucumber.api.java.en.Given;
import cucumber.api.java.en.Then;
import cucumber.api.java.en.When;
import cucumber.api.Scenario;
import cucumber.api.java.After;
import cucumber.api.java.Before;

import java.util.List;
import java.util.concurrent.TimeUnit;

import org.junit.Assert;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

public class Stepdefs {

    WebDriver driver;

    @Before
    public void beforeScenario(Scenario scenario) {
        System.out.println("### Testing scenario '" + scenario.getName() + "' ###");
        System.setProperty("webdriver.chrome.driver", "C:/share/chromedriver.exe");
        driver = new ChromeDriver();
        driver.manage().timeouts().implicitlyWait(20, TimeUnit.SECONDS);
        driver.manage().window().maximize();
    }

    @After
    public void afterScenario() {
        System.out.println("Stop ChromeDriver");
        driver.quit();
    }

    @Given("the gyrogame home page is displayed")
    public void gyrogame_blog_displayed() {
        String baseUrl = "https://gyrogame.de";
        driver.get(baseUrl);
    }

    @When("I click on the custom link labeled {string}")
    public void click_on_custom_link(String label) {
        WebElement menu = driver.findElement(By.id("menu-links"));
        List<WebElement> links = menu.findElements(By.tagName("li"));
        for (int i = 0; i < links.size(); i++) {
            if (links.get(i).findElement(By.tagName("a")).getText().contains(label)) {
                driver.get(links.get(i).findElement(By.tagName("a")).getAttribute("href"));
                return;
            }
        }
        Assert.fail("custom link not found");
    }

    @When("The first element with the class name {string} is visible")
    public void element_visible(String classname){
        WebDriverWait wait = new WebDriverWait(driver, 10);
        wait.until(ExpectedConditions.visibilityOfElementLocated(By.className(classname)));
    }

    @Then("the page title should contain {string}")
    public void the_page_title_should_contain(String titleContains) {
        Assert.assertTrue("Title does not contain " + titleContains, driver.getTitle().contains(titleContains));
    }

}
