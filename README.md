﻿# Vaetech.Collections.Generic

[![Join the chat at (https://badges.gitter.im/Vaetech-Collections-Generic)](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Vaetech-Collections-Generic/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

|    Package    |Latest Release|
|:--------------|:------------:|
|**Vaetech.Collections.Generic**    |[![NuGet Badge Vaetech.Collections.Generic](https://buildstats.info/nuget/Vaetech.Collections.Generic)](https://www.nuget.org/packages/Vaetech.Collections.Generic)

|   Platform   |Build Status|
|:-------------|:----------:|
|**Windows**   |[![Build status](https://ci.appveyor.com/api/projects/status/i2d63hox0k4wxsn4?svg=true)](https://ci.appveyor.com/project/cochachyLE-Eng/vaetech-collections-generic)|
|**Linux**     |[![Build status](https://ci.appveyor.com/api/projects/status/i2d63hox0k4wxsn4?svg=true)](https://ci.appveyor.com/project/cochachyLE-Eng/vaetech-collections-generic)|

## What is Vaetech.Collections.Generic?

Vaetech.Collections.Generic is a C# library that can be used to create custom collections and convert them to `ArrayList<Key,Value>` collection or `Parameters` Collection. It is an alternative to the data dictionary, with the option to add multiple values and receive notifications when the data source changes.

## License Information

```
MIT License

Copyright (C) 2021-2022 .NET Foundation and Contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
```

## Installing via NuGet

The easiest way to install Vaetech.Collections.Generic is via [NuGet](https://www.nuget.org/packages/Vaetech.Collections.Generic/).

In Visual Studio's [Package Manager Console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console),
enter the following command:

    Install-Package Vaetech.Collections.Generic

## Getting the Source Code

First, you'll need to clone Vaetech.Collections.Generic from my GitHub repository. To do this using the command-line version of Git,
you'll need to issue the following command in your terminal:

    git clone --recursive https://github.com/cochachyLE-Eng/Vaetech.Collections.Generic.git

## Updating the Source Code

Occasionally you might want to update your local copy of the source code if I have made changes to Vaetech.Collections.Generic since you
downloaded the source code in the step above. To do this using the command-line version fo Git, you'll need to issue
the following commands in your terminal within the Vaetech.Collections.Generic directory:

    git pull
    git submodule update

## Building

In the top-level Vaetech.Collections.Generic directory, there are a number of solution files; they are:

* **Vaetech.Collections.Generic.sln** - includes projects for .NET 4.5/4.6.1/4.8, .NETStandard 2.1 as well as the unit tests.

Once you've opened the appropriate Vaetech.Collections.Generic solution file in [Visual Studio](https://www.visualstudio.com/downloads/),
you can choose the **Debug** or **Release** build configuration and then build.

Both Visual Studio 2017 and Visual Studio 2019 should be able to build Vaetech.Collections.Generic without any issues, but older versions such as
Visual Studio 2015 will require modifications to the projects in order to build correctly.

## Using Vaetech.Collections.Generic

### How can it be used?

It only takes a few lines of code to convert collections to `ArrayList<Key,Value>` collection or `Parameters` Collection.

```csharp
// Parameters Test
Parameters parameters = new Parameters();
parameters.Add("key", "key...");
parameters.Add("x-amz-credential", "x-amz-credential...");
parameters.Add("x-amz-algorithm", "x-amz-algorithm...");
parameters.Add("x-amz-date", "x-amz-date...");
parameters.Add("x-amz-signature", "x-amz-signature...");
parameters.Add("policy", "policy...");
parameters.Add("acl", "acl...");
parameters.Add("Content-Type", "Content-Type...");
parameters.Add("success_action_status", "success_action_status...");           

// Convert List<Parameter> to ArrayList<TKey,TValue>. (sample 1)
var result = parameters.ToArrayList(k => k.Name, v => v.Value.ToString());

// Convert List<Parameter> to ArrayList<TKey,TValue1,TValue2>. (sample 2).
var result2 = parameters.ToArrayList(k => k.Name, v1 => v1.Value, v2 => v2.DbType);

string key = result["key"];
string x_amz_credential = result["x-amz-credential"];
string x_amz_algorithm = result["x-amz-algorithm"];
string x_amz_date = result["x-amz-date"];
string x_amz_signature = result["x-amz-signature"];
string policy = result["policy"];
string acl = result[key: "acl"];
string content_Type = result[key: "Content-Type"];
string success_action_status = result[key: "success_action_status"];

Assert.AreEqual(key, parameters.Where(c => c.Name == "key").FirstOrDefault().Value);
Assert.AreEqual(x_amz_credential, parameters.Where(c => c.Name == "x-amz-credential").FirstOrDefault().Value);
Assert.AreEqual(x_amz_algorithm, parameters.Where(c => c.Name == "x-amz-algorithm").FirstOrDefault().Value);
Assert.AreEqual(x_amz_date, parameters.Where(c => c.Name == "x-amz-date").FirstOrDefault().Value);
Assert.AreEqual(x_amz_signature, parameters.Where(c => c.Name == "x-amz-signature").FirstOrDefault().Value);
Assert.AreEqual(policy, parameters.Where(c => c.Name == "policy").FirstOrDefault().Value);
Assert.AreEqual(acl, parameters.Where(c => c.Name == "acl").FirstOrDefault().Value);
Assert.AreEqual(content_Type, parameters.Where(c => c.Name == "Content-Type").FirstOrDefault().Value);
Assert.AreEqual(success_action_status, parameters.Where(c => c.Name == "success_action_status").FirstOrDefault().Value);
```

