# Introduction	

### Technologies Used:

+ **Operating System:** Windows 10
+ **IDE:** Visual Studio 2019
+ **Programming Language:** C#
+ **Libraries Used:** Selenium, TweetSharp

### Summary:
TechCrunch Tweeter is a program written in C# that will tweet the featured article that is on [TechCrunch](https://techcrunch.com/) each morning at 9am. The program uses a Selenium script to scrape [TechCrunch](https://techcrunch.com/) and grab the featured articles title, url and image (where available). It will then compose a tweet with a message like 'New featured article -> <ArticleTitle> take a look here: <ArticleLink>' (it randomly picks 1 of 10 different tweet messages). It will download the featured article image and tweet that out with the randomly chosen tweet. I used the TweetSharp library to use C# code to compose a tweet and tweet it for me.
  
  # General
  
 ## Twitter Link  <img src="Images/Icons/twitterIcon.png" height = "20" width = "20" alt="Twitter Icon" />
 Take a look at the Twitter account here: https://twitter.com/TechC_Daily
 
  ## Sample Tweet
  <img src="Images/tweet.PNG" alt="Tweet" href = "https://twitter.com/TechC_Daily/status/1159487020038787072" />
  
 # Technical Components:
 
### Developer account on Twitter:
In order to tweet using C# I had to create a twitter developer account stating my reasons for needing the developer account and agreeing to their terms of service. I was then provided with my api key and access token that allowed me to tweet with an api call.

### Selenium Script
The selenium script that I wrote launches a google chrome browser and navigates to [TechCrunch](https://techcrunch.com/). It then grabs the featured articles title and featured image as well as the url for the article. It downloads the image locally so I can use it in the tweet. Sometimes [TechCrunch](https://techcrunch.com/) will feature a video instead of an image, when this happens the selenium script will just grab the article title and article link instead.

### Tweet Sharp
Tweet Sharp is a C# library that I used in this project, this library allows me to compose a tweet with images (media tweet) or without. I am then able to pass my api key and access token to the api which authenticates the request and tweets out the composed tweet. 

### Batch file
I wrote a simple batch file which is used to run the C# code each morning. The batch file runs 'TechcrunchTweeter.exe' which executes the program.

### Scheduled task
I created a scheduled task on my pc which runs the bat file at 9am every morning.

 # Potential Improvements:
 +  Add more Tweet Messages (there are only 10 right now)
 +  Run Chrome in suppressed mode to stop it showing up when run
 +  Delete Images locally after tweeting to save memory
 
  # Contact details:
  <img src="Images/Icons/LinkedInIcon.png" height = "35" width = "35" alt="LinkedIn Icon" /> https://www.linkedin.com/in/gary-mchugh-b42037126/
  
  <img src="Images/Icons/emailIcon.png" height = "35" width = "35" alt="Email Icon" /> <a href="mailto:garymchughsoftware@outlook.com">garymchughsoftware@outlook.com</a>
  
 ### Disclaimer
 I am in no way affiliated with [TechCrunch](https://techcrunch.com/). This project was simply written as a coding exercise for myself to iprove my knowledge and skills.
