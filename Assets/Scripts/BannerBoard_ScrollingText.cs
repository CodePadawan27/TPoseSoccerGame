using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;
using System;

public class BannerBoard_ScrollingText : MonoBehaviour
{

    public float speed = 2.0f;
    private Animator _scrollingAnimation;
    private TextMesh _bannerBoardTextMesh;
    private string _bannerBoardText;
    private List<string> _parsedLocationJsonData = new List<string>();
    private List<string> _parsedWeatherJsonData = new List<string>();
    private string _locationURL = "http://ip-api.com/json";

    private bool jsonWeatherFetched = false;
    private bool internetAvailable = false;

    private float _latitude;
    private float _longitude;
    private float _temperature;
    private string _city;
    private string _country;

    void Start()
    {
        _scrollingAnimation = GetComponent<Animator>();
        _bannerBoardTextMesh = GetComponent<TextMesh>();

        //If network connection is available
        if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            internetAvailable = true;
            WWW wwwLocation = new WWW(_locationURL);
            StartCoroutine(RequestAndParseJSONLocationData(wwwLocation));
        }
    }

    public IEnumerator PlayAnimationWithDifferentLength()
    {
        List<string> bannerboardWithInternetTexts = new List<string> {
            "Current outside temperature is " + Math.Round(_temperature, 0).ToString() + " degrees",
            "Ah, you live in " + _city + ", " + _country,
            "Hello world",
            "All your base are belong to us",
            "It's a hard knock life",
            "Happy little trees",
        };

        List<string> bannerboardWithNoInternetTexts = new List<string> {
            "Hello worlds",
            "Sacre bleuau",
            "Mon dieau",
            "You are currently offline btw",
            "Happy little trees",
            "All your base are belong to us"
        };

        if (internetAvailable)
        {
            while (true)
            {
                //print("1");
                System.Random r = new System.Random();
                _bannerBoardTextMesh.text = bannerboardWithInternetTexts[r.Next(bannerboardWithInternetTexts.Count)];
                //just for testing _bannerBoardTextMesh.text = bannerboardWithInternetTexts[0];

                if (_bannerBoardTextMesh.text.Length >= 12)
                {
                    //print("2");
                    _scrollingAnimation.ResetTrigger("pla");
                    _scrollingAnimation.Play("BannerText_scrolling_largeText");
                    yield return new WaitForSeconds(_scrollingAnimation.GetCurrentAnimatorStateInfo(0).length);
                }
                else
                {
                    //print("3");
                    _scrollingAnimation.ResetTrigger("pla2");
                    _scrollingAnimation.Play("BannerText_scrolling_smallText");
                    yield return new WaitForSeconds(_scrollingAnimation.GetCurrentAnimatorStateInfo(0).length);
                }
                yield return new WaitForSeconds(_scrollingAnimation.GetCurrentAnimatorStateInfo(0).length);
            }
        }
        else
        {
            while (true)
            {
                print("1");
                System.Random r = new System.Random();
                _bannerBoardTextMesh.text = bannerboardWithNoInternetTexts[r.Next(bannerboardWithNoInternetTexts.Count)];

                if (_bannerBoardTextMesh.text.Length >= 12)
                {
                    print("2");
                    _scrollingAnimation.ResetTrigger("pla");
                    _scrollingAnimation.Play("BannerText_scrolling_largeText");
                    yield return new WaitForSeconds(_scrollingAnimation.GetCurrentAnimatorStateInfo(0).length);
                }
                else
                {
                    print("3");
                    _scrollingAnimation.ResetTrigger("pla2");
                    _scrollingAnimation.Play("BannerText_scrolling_smallText");
                    yield return new WaitForSeconds(_scrollingAnimation.GetCurrentAnimatorStateInfo(0).length);
                }
                yield return new WaitForSeconds(_scrollingAnimation.GetCurrentAnimatorStateInfo(0).length);
            }
        }
    }

    IEnumerator RequestAndParseJSONLocationData(WWW address)
    {
        char[] delimiterChars = { ',', ':' };
        List<string> removableStrings = new List<string> {"as", "city", "country", "countryCode", "isp", "lat", "lon", "org",
            "query", "region", "regionName", "status", "success", "timezone", "zip"};
        yield return address;

        if (address.error == null)
        {
            _parsedLocationJsonData = address.text.Replace("\"", "")
                .Replace("{", "")
                .Replace("}", "")
                .Split(delimiterChars).ToList();
            _parsedLocationJsonData.RemoveAll(x => removableStrings.Contains(x));

            PopulateJSONLocationVariables();

            WWW wwwWeather = new WWW("http://api.openweathermap.org/data/2.5/weather?&units=metric&lat=" + _latitude.ToString() + "&lon=" +
                _longitude.ToString() + "&appid=bc2ed74ec5b3400ab4240480cfbc73f6");
            StartCoroutine(RequestAndParseWeatherJSONData(wwwWeather));
        }
        else
        {
            Debug.Log("Error, domo arigato mister roboto");
        }

    }

    //Populates the global variables with parsed JSON location data
    void PopulateJSONLocationVariables()
    {
        _city = _parsedLocationJsonData[1];
        _country = _parsedLocationJsonData[2];
        _latitude = float.Parse(_parsedLocationJsonData[5]);
        _longitude = float.Parse(_parsedLocationJsonData[6]);
    }

    //Requests weather data from api.openweathermap, uses latitude and longitude coordinates in API-call
    IEnumerator RequestAndParseWeatherJSONData(WWW address)
    {
        char[] delimiterChars = { ',', ':' };
        List<string> removableStrings = new List<string> {"coord", "lon", "lat", "weather", "id", "main", "description", "icon",
            "base", "main", "temp", "pressure", "humidity", "temp_min", "temp_max", "sea_level", "grnd_level", "wind", "speed", "deg", "clouds", "all", "dt", "sys", "message",
            "country", "sunrise", "sunset", "id", "name", "cod"};

        yield return address;

        if (address.error == null)
        {
            _parsedWeatherJsonData = address.text.Replace("\"", "")
                .Replace("{", "")
                .Replace("}", "")
                .Replace("[", "")
                .Replace("]", "")
                .Split(delimiterChars).ToList();
            _parsedWeatherJsonData.RemoveAll(x => removableStrings.Contains(x));

            _temperature = float.Parse(_parsedWeatherJsonData[7]);
            jsonWeatherFetched = true;
        }
        else
        {
            Debug.Log("Error, domo arigato mister roboto");
        }
    }
}
