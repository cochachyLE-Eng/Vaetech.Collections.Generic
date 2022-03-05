﻿# Vaetech.Collections.Generic

[![Join the chat at (https://badges.gitter.im/Vaetech-Collections-Generic)](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Vaetech-Collections-Generic/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

|    Package    |Latest Release|
|:--------------|:------------:|
|**Vaetech.Collections.Generic**    |[![NuGet Badge Vaetech.Collections.Generic](https://buildstats.info/nuget/Vaetech.Collections.Generic)](https://www.nuget.org/packages/Vaetech.Collections.Generic)

|   Platform   |Build Status|
|:-------------|:----------:|
|**Windows**  |[![Build status](https://ci.appveyor.com/api/projects/status/19qpx965dh2s3lp1?svg=true)](https://ci.appveyor.com/project/cochachyLE-Eng/vaetech-powershell)|

## What is Vaetech.Collections.Generic?

Vaetech.Collections.Generic is a C# library which may be used for ... 

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

It only takes a few lines of code to convert collections to ArrayList<TKey,TValue>.

```csharp
// Paramters
Request request = new Request();
request.AddParameter("key", "key...");
request.AddParameter("x-amz-credential", "x-amz-credential...");
request.AddParameter("x-amz-algorithm", "x-amz-algorithm...");
request.AddParameter("x-amz-date", "x-amz-date...");
request.AddParameter("x-amz-signature", "x-amz-signature...");
request.AddParameter("policy", "policy...");
request.AddParameter("acl", "acl...");
request.AddParameter("Content-Type", "Content-Type...");
request.AddParameter("success_action_status", "success_action_status...");

// Convert List<Parameter> to ArrayList<TKey,TValue>.
var result = request.Parameters.ToArrayList(k => k.Name, v => v.Value);

var key = result["key"].Value1;
var x_amz_credential = result["x-amz-credential"].Value1;
var x_amz_algorithm = result["x-amz-algorithm"].Value1;
var x_amz_date = result["x-amz-date"].Value1;
var x_amz_signature = result["x-amz-signature"].Value1;
var policy = result["policy"].Value1;
var acl = result["acl"].Value1;
var content_Type = result["Content-Type"].Value1;
var success_action_status = result["success_action_status"].Value1;

Assert.AreEqual(key, request.Parameters.Where(c => c.Name == "key").FirstOrDefault().Value);
Assert.AreEqual(x_amz_credential, request.Parameters.Where(c => c.Name == "x-amz-credential").FirstOrDefault().Value);
Assert.AreEqual(x_amz_algorithm, request.Parameters.Where(c => c.Name == "x-amz-algorithm").FirstOrDefault().Value);
Assert.AreEqual(x_amz_date, request.Parameters.Where(c => c.Name == "x-amz-date").FirstOrDefault().Value);
Assert.AreEqual(x_amz_signature, request.Parameters.Where(c => c.Name == "x-amz-signature").FirstOrDefault().Value);
Assert.AreEqual(policy, request.Parameters.Where(c => c.Name == "policy").FirstOrDefault().Value);
Assert.AreEqual(acl, request.Parameters.Where(c => c.Name == "acl").FirstOrDefault().Value);
Assert.AreEqual(content_Type, request.Parameters.Where(c => c.Name == "Content-Type").FirstOrDefault().Value);
Assert.AreEqual(success_action_status, request.Parameters.Where(c => c.Name == "success_action_status").FirstOrDefault().Value);
```

