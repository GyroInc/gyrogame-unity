Feature: GyroGame Blog Website

Scenario: Custom Links
    Given the gyrogame home page is displayed
    When I click on the custom link labeled "YouTrack Project Management"
    And The first element with the class name "yt-search-panel" is visible
    Then the page title should contain "YouTrack"

    Given the gyrogame home page is displayed
    When I click on the custom link labeled "GitHub – Unity Project"
    Then the page title should contain "Manut38/gyrogame-unity"

    Given the gyrogame home page is displayed
    When I click on the custom link labeled "GitHub – Controller Firmware"
    Then the page title should contain "Manut38/gyrogame-hardware"

