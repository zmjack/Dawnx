## JSend

It's descripted on [http://labs.omniti.com/labs/jsend/wiki](http://labs.omniti.com/labs/jsend/wiki). And we Implement **JSend** in **Dawnx**.



### **Introduction**

- **What?** - Put simply, JSend is a specification that lays down some rules for how [JSON](http://json.org/) responses from web servers should be formatted. JSend focuses on application-level (as opposed to protocol- or transport-level) messaging which makes it ideal for use in [REST](http://en.wikipedia.org/wiki/Representational_State_Transfer)-style applications and APIs.

- **Why?** - There are lots of web services out there providing JSON data, and each has its own way of formatting responses. Also, developers writing for [JavaScript?](http://labs.omniti.com/labs/jsend/wiki/JavaScript) front-ends continually re-invent the wheel on communicating data from their servers. While there are many common patterns for structuring this data, there is no consistency in things like naming or types of responses. Also, this helps promote happiness and unity between backend developers and frontend designers, as everyone can come to expect a common approach to interacting with one another.

- **Hold on now, aren't there already specs for this kind of thing?** - Well... no. While there are a few handy specifications for dealing with JSON data, most notably [Douglas Crockford](http://www.crockford.com/)'s [JSONRequest](http://www.json.org/JSONRequest.html) proposal, there's nothing to address the problems of general application-level messaging. More on this later.

- **(Why) Should I care?** - If you're a library or framework developer, this gives you a consistent format which your users are more likely to already be familiar with, which means they'll already know how to consume and interact with your code. If you're a web app developer, you won't have to think about how to structure the JSON data in your application, and you'll have existing reference implementations to get you up and running quickly.

- **Discuss** - [Mailing list](http://lists.omniti.com/mailman/listinfo/jsend-users)



### How's is work?

A basic JSend-compliant response is as simple as this:

```json
{
    status : "success",
    data : {
        "post" : { 
            "id" : 1,
            "title" : "A blog post", 
            "body" : "Some useful content"
        }
     }
}
```

When setting up a JSON API, you'll have all kinds of different types of calls and responses. JSend separates responses into some basic types, and defines required and optional keys for each type:

| **Type** | **Description**                                              | **Required Keys** | **Optional Keys** |
| -------- | ------------------------------------------------------------ | ----------------- | ----------------- |
| success  | All went well, and (usually) some data was returned.         | status, data      |                   |
| fail     | There was a problem with the data submitted, or some pre-condition of the API call wasn't satisfied | status, data      |                   |
| error    | An error occurred in processing the request, i.e. an exception was thrown | status, message   | code, data        |



### Use JSend With Dawnx

#### Success simple

For the basic JSend-compliant response, we can create a instance of **JSend** like this:

```C#
using Dawnx;

...
    JSend.Success.Create(new
    {
        post = new
        { 
            id = 1,
            title = "A blog post", 
            body = "Some useful content",
        },
    });
...
```

If you want to send a response with no data, try to code this simply:

```C#
using Dawnx;

...
    JSend.Success.Create();
...
```

#### Fail simple

```C#
using Dawnx;

...
    JSend.Fail.Create();
...
```

#### Error simple

```C#
using Dawnx;

...
    JSend.Error.Create();
...
```



