# ForecastFavor Web Application 

### Semester Long Final Project (MAPD2023)

1. Task 1
   - [x] To Make the ForecastFavorApp in the Web version for the CMP-1005 - Advanced Web Development Project


> [!NOTE]
> The application is responsible for displaying the forecast data related to the specific location entered by the user.
> > API used in the application : Weather API (http://api.weatherapi.com/v1/forecast.json?key=bcea99f817174902bdc03259230311&q=sudbury&days=1&aqi=no&alerts=no)

### Team Members:
1. Dennis Padilla (A00275114)
2. Ruby Chaulagain (A00278592)
3. Sreenath Segar (A00255694)

### Code Structure
This project consists of two branches: 
> main (worked on by Dennis & Sreenath)
> feature_ui (worked on by Ruby)

* The main branch has the core functionality of searching locations and getting the respective weather data with the text to speech feature, which was one of our ancilliary goals.
* It also has the functionality of "Read Weather Aloud" feature, which dictates the displayed weather condition.
* Has unit tests and integration tests
* Has the user notification feature, which gives personalized weather based suggestions/notifications in a pop up box.


* feature_ui branch is mostly UI for all the views such as Today view, Tomorrow view, 3 Day Forecast View, Users View, User Preferences View
* It also has CRUD functionality for users and user preferences.
* Google Maps has been added to each weather forecast view. It correctly displays the map of the location entered by the user.
* Consists of a global searchbar that allows user to search for any location around the world, and the respective data gets displayed in the UI.
* The main reason behind creating a new branch for ui was to segregate the ui changes with the major backend changes, so it would not conflict in between and prevent project from failing. 
* This branch is up to date with the main branch but ahead of main branch. It has full functionality of the application fetched from the main branch combined with the feature_ui branch. However, it is not merged yet.
* The tests written in the main branch no longer works for the feature_ui branch as the UI elements have been changed. Hence, this branch has all the UI changes and core functionality of the app but we didnt have enough time to modify the tests on this branch. 
* Checking out the feature_ui branch will give access to observing the full functionality of the app. 

# ForecastFavor Home Page
<img width="1439" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/e728af4d-12ca-48c5-a89b-2c7ccdf3c95d">

# Current view of Weather Forecast Landing Page (Today View)
<img width="1434" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/757ad153-6532-46d0-a4c9-70325ae2c6c2">
<img width="1440" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/7e383266-6205-4b29-b233-197e9ee2c987">

# Current view of Tomorrow Page 
<img width="1438" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/49b9b5d9-67bc-4034-8f38-7d586f895651">
<img width="1438" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/6a5ae7e7-6dde-4498-8c8f-b1bafb7c80a1">

# Current View of 3 Day Forecast Page
<img width="1438" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/5b45c84d-b887-4f08-83bd-a9642553f665">

# User Preferences
<img width="1438" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/de6dad55-3f59-4114-bba0-968e88177c9f">

## Create User Preferences
<img width="1432" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/7a4e7ac2-d7d8-43d7-ac6f-d1465a814660">

## Edit User Preferences
<img width="1432" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/4056e85b-33dc-40c3-b7da-5b94c6e9e711">

## Delete User Preferences
<img width="1432" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/bff3e25c-13b1-4bd3-852b-96823865fcb6">

# Application Users
<img width="1432" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/6a7d07cb-53d4-4932-bb7a-33f90484cd8c">

## Creating new User
<img width="1432" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/41aeb131-80cb-4a18-84bd-ebc9bdcc2d07">

## Details of User
<img width="1432" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/1c2c58c1-05cf-461a-973d-bb38235e81f1">

## Delete User
<img width="1432" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/ea39c79b-1d98-463d-9af3-d2c37fcf772b">

# Notifications based on the weather condition 
<img width="366" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/37743aae-304c-4f27-b4fc-27c24a350bce">

# Speech Recognition to get weather from desired city in Today Page
<img width="1355" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/dda051be-31c1-41d0-8d3f-c2b3d96b8781">

#### Hourly forecast for the desired city along with its map
<img width="1347" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/05c2ccb8-36c9-47a9-b6e3-377efb9ec176">

# Speech Recognition to get weather from desired city in Tomorrow Page 
<img width="1347" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/fa7ca32a-75fc-4b00-b4d5-5a90888352c8">
<img width="1347" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/d0907775-704f-4211-8456-d0141a69035e">

# Speech Recognition to get weather from desired city in 3 Day Forecast Page 
(zoomed out to fit in the snapshot)
<img width="1350" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/2b270fb3-b803-4685-b13a-e499dcbf1086">

# Global Search Bar to search for locations by text
(Here's the weather from my hometown Kathmandu)
<img width="1350" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/ec587245-b2b9-4c6f-933e-b9793d3e6992">
<img width="1350" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/a19df756-9ad4-470a-ab43-fec3d36e19a9">

## App's Responsiveness in various Responsive Design Mode
### Today View
<img width="784" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/59fe0836-918f-421e-9d20-e1a91ef9813e">
<img width="271" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/5c02a255-7a72-4755-8513-9232b2a3ff7a">

- [ ] app's responsiveness while clicking the hamburger
      <img width="271" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/e1031151-0bfb-4ae1-b4c7-1254ea40c97a">


### Tomorrow View
<img width="271" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/c02a7383-448f-4168-964f-472d798e3dde">

### 3 Day Weather Forecast View
<img width="271" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/6c9f656a-fba1-4ab1-b58d-c7e50bcbcd9b">
<img width="271" alt="image" src="https://github.com/Cambrian-MAPD-May2023/ForecastFavorApp_Web/assets/74127503/d76efde8-33ae-46b7-a8b6-93c7948ef424">






