# BlazorStyleSheet

**BlazorStyleSheet** allows blazor (**Blazor server**, **webassembly** and **MAUI** blazor) developers to write their CSS styles directly in C# without the need for external stylesheets.

**BlazorStyleSheet** is built on top of **Stylesheet.NET**.

**Stylesheet.Net** is a cross-platform .NET library designed for C#, VB.NET, and F# developers. It enables developers to write CSS styles directly in their code, eliminating the need for external stylesheet files. **Stylesheet.Net** provides pre-written implementations for all CSS properties, at-rules, keywords, and other elements, eliminating the need for additional dependencies.

> **Stylesheet.Net** is not a library from outside. It is an open source (MIT) library I wrote and maintain.

**Repository**: https://github.com/AbdisamadMoh/Stylesheet.NET

I would recommend you to have a look and understand how to use **Stylesheet.Net** before you can continue this tutorial. Click the link above and read the documentations.

![https//githubcom/AbdisamadMoh/StylesheetNET/raw/master/20240304013616imagepng](https://github.com/AbdisamadMoh/Stylesheet.NET/raw/master/2024-03-04-01-36-16-image.png)

## Quick start

To start using **BlazorStylesheet**, we first need to add it in our project.

We can add it from **nuget**, or directly referencing **BlazorStylesheet.dll**

Open `nuget` package manager in **visual studio** and paste the following code.

```csharp
Install-Package BlazorStylesheet -Version 1.0.0
```

1. After installation done, open the file **Program.cs** or **Startup.cs** or **MauiProgram.cs** in your blazor project.
   
   **Add the following code**
   
   ```csharp
   using BlazorStylesheet;
   builder.Services.AddStylesheet();
   ```

2. Open the file **_Imports.razor**. In **blazor server** and **webassembly** you can find it under the project root. For **MAUI** you can find it under **Components** folder.

**Add the following namespaces**

```csharp
@using BlazorStylesheet
@using StylesheetNET
```

3. Now open **_Layout.cshtml** in **blazor server** which you can find it under **Shared** folder, or **index.html** in **blazor webassembly** and **MAUI** which you can find it under **wwwroot** folder.
   
   **Add the following html tags in the head before any script or styles.**
   
   ```html
   <link href="_content/BlazorStylesheet/fix.min.css" rel="stylesheet" />
   <script src="_content/BlazorStylesheet/BlazorStylesheet.js"></script>
   ```
   
   ![](https://imgur.com/zzdmRcX.png)
   
   **Now scroll up, in the tag <htm ...> add *loading="loader"*. I will explain why we need these tags and attributes in later sections (see section 'Why we need...').**
   
   ```csharp
   loading="loader"
    OR
   loading=""
   ```
   
   ![](https://imgur.com/3Y767Gj.png)

4. And lastly, in **Blazor Server** and **Blazor Webassembly** open **App.razor** which you can find under the project **root**. And in **MAUI**, open **Routes.razor** which you can find under **Components** folder. 
   
   **Wrap everything you see there with:** 
   
   ```cshtml
   <RazorStylesheet> </RazorStylesheet>
   ```
   
   ![](https://imgur.com/AW5x14N.png)

> You have to complete every step mentioned above. 

### Now we are ready to write our first css in C#.

in our **MainLayout.razor**, let's inject the **main stylesheet** and call it `sheet`. Then we can use `sheet` property to write our CSS. You can do this in every component you want to access the **main stylesheet**.

Now copy the following code and paste in **MainLayout.razor** C# code.

```csharp
 [Inject]
 public Stylesheet sheet
 {
     get; set;
 }

 protected override void OnAfterRender(bool firstRender)
 {
    base.OnAfterRender(firstRender);

    if (firstRender)
    {
        sheet["body"].BackgroundColor = "blue";
        sheet.Build();
    }

 }
```

Now build your application and refresh the browser. You will see your html body became `blue` color.

To learn how to write your stylesheet in C# please refer to https://github.com/AbdisamadMoh/Stylesheet.NET#quick-start

To access `sheet `in other components, `inject` them the `stylesheet`, following **step 4** above.

> Whenever you change something in your `stylesheet`, you have to call `sheet.Build()` to reflect the changes in the `DOM`.

That is how you can access your `stylesheet` in your components. But writing your whole css in a component is not a good idea as that makes your component codes unreadable.

Infact there is nothing wrong writing your css in your components. But it is good idea to write in a separate `classes`. We will do that in the next section. Also we will learn when we should write our css in a component.

### Lets build real world application

Now lets cretate the following **navbar** menu.

![](https://imgur.com/vFkjofP.png)

1. Create a new component and name it **NavBar.Razor**.

2. Paste the following `html` code in the **NavBar.Razor** you have created.

```cshtml
<nav class="navbar">
    <a href="#" class="selected">Home</a>
    <a href="#">About</a>
    <a href="#">Blog</a>
    <a href="#">Portefolio</a>
    <a href="#">Contact</a>
</nav>
```

3. Add the **NavBar.Razor** you have created to **MainLayout.razor** or in any other component you want.
   
   ![NavBar](https://imgur.com/S4qjYMq.png)

4. Now lets create our CSS in C#. Create a new class and call it `Style.cs` and add these two namespaces.

```csharp
using BlazorStylesheet;
using StylesheetNET;
```

Now, our class may look somehing like this:

```csharp
public class Style
{
}
```

But that's just a simple class, and **BlazorStylesheet** doesn't recognize it. We need to indicate that this class is for **stylesheet**. We can do that by simply decorating it with `[StylesheetClass]` attribute like this:

```csharp
[StylesheetClass]
public class Style
{ 
}
```

Now, when we run our application, **BlazorStylesheet** will recognize our class and compile it into CSS. 

Now let's write some CSS in our class. **But write where?**

Well, lets add a method and name it `Setup` and decorate it with `[StylesheetMethod]` like this:

```csharp
[StylesheetMethod]
private void Setup()
{

}
```

When we run our application again and **BlazorStylesheet** finds our class, it will look for our `Setup` method and executes it.

Infact, our `Setup` method is executed, not because it is called `Setup` but **BlazorStylesheet** looks for any parameterless methods that are decorated with `[StylesheetMethod]` attribute and executes them. That means we can have as many methods as we want and name whatever names we want.

```csharp
[StylesheetMethod]
public void method1()
{
    // will be executed
} 

public method2()
{
    // will not be executed because it is missing [StylesheetMethod]
   // BlazorStylesheet will ignore it.
} 


[StylesheetMethod]
public void method3(object parameter)
{
    // will throw an exception error 
   // bacause a method with [StylesheetMethod]
   // should not have any parameter. 
}
```

**But still, how we supposed to write our CSS?**

Well, to write our CSS we need to have an access to our **main stylesheet**. The **main stylesheet** is the stylesheet that acts like the **stylesheet file** of our website. It is created when the browser loads our website. And its available throughout our website and it is constant, meaning, it never gets recreated again untill the browser is refreshed. 

In **razor components** we can access it through injection. 

But to access the **main stylesheet** in our class, we have to add a property of type `Stylesheet` and decorate it with `[StylesheetProperty]`. We can name it whatever name we want. For this example, we will name it `sheet`.

```csharp
 [StylesheetClass]
 public class Style
 {
     [StylesheetProperty]
     private Stylesheet sheet
     {
         get;
         set;
     }
 }
```

Now, `sheet` is a reference to our **main stylesheet**. And we can use it to write our CSS.

**BlazorStylesheet** will look for any property with attribute `[StylesheetProperty]` and reference them to the **main stylesheet**.

Now, to check if everything is working like expected.  Let's give our website's  **body** `red background color`. 

Your `Style.cs` class should look like this for now.

```csharp
 [StylesheetClass]
 public class Style
 {
     [StylesheetProperty]
     public Stylesheet sheet
     {
         get;
         set;
     }

     [StylesheetMethod]
     private void MakeBodyRed()
     {
         sheet["body"] = new Element()
         {
             BackgroundColor = "red !important"
         };
       //You can also write like this: 
       // sheet["body"].BackgroundColor = "red !important";
      // But the way i wrote is recommended and cleaner if you are not updating.
     }
 }
```

Now build your application and refresh the browser. Your website body should be red.

> In **MAUI**, you may encounter this error:
> 
>  `'Element' is an ambiguous reference between 'Microsoft.Maui.Controls.Element' and 'StylesheetNET.Element'` 
> 
> Because both `Microsoft.Maui.Controls` and `StylesheetNET` have `Element` class. 
> 
> **To fix, Add the following namespace:**
> 
> ```csharp
> using Element = StylesheetNET.Element;
> ```
> 
> ![image](https://imgur.com/ZratGZV.png)

##### If everything is going right, let's write the CSS for our `Navbar.razor`

Although you can write your CSS in anyway you like, but it is good idea to categorize them in methods. That is what we will do here.

Now let's write our full CSS. Here is the full C# code.

```csharp
  [StylesheetClass]
  public class Style
  {
      [StylesheetProperty]
      private Stylesheet sheet
      {
          get;
          set;
      }

      [StylesheetMethod]
      private void NavBar()
      {
          sheet[".navbar"] = new Element()
          {

              Position = PositionOptions.Relative,
              Width = "590px",
              Height = "60px",
              PaddingLeft = "10px",
              PaddingRight = "10px",
              BackgroundColor = "#34495e",
              BorderRadius = "8px",
              FontSize = "0"
          };
      }
      [StylesheetMethod]
      private void NavBar_a()
      {
          sheet[".navbar > a"] = new Element()
          {
              LineHeight = "50px",
              Height = "100%",
              Width = "100px",
              FontSize = "15px",
              Display = DisplayOptions.InlineBlock,
              Position = PositionOptions.Relative,
              ZIndex = "1",
              TextDecoration = "none",
              TextTransform = TextTransformOptions.Uppercase,
              TextAlign = TextAlignOptions.Center,
              Color = "white",
              Cursor = CursorOptions.Pointer
          };
      }
      [StylesheetMethod]
      private void NavBar_a_Selected()
      {
          sheet[".navbar > a.selected"] = new Element()
          {
              BackgroundColor = "#17B1EA",
              BorderRadius = "10px"

          };
      }
      [StylesheetMethod]
      private void NavBar_a_Selected_Hover()
      {
          sheet[".navbar > a"] = new ElementHover()
          {
              BackgroundColor = "#17B1EA",
              BorderRadius = "10px",
              Transition = "border-radius",
              TransitionDuration = ".3s",
              TransitionTimingFunction = TransitionTimingFunctionOptions.EaseIn

          };
      }
      [StylesheetMethod]
      void Animation()
      {
          sheet["h1"] = new Element()
          {
              AnimationName = "pulse",
              AnimationDuration = "2s",
              AnimationIterationCount = AnimationIterationCountOptions.Infinite
          };

          sheet[AtRuleType.Keyframes] = new Keyframes("pulse")
          {
              ["from"] = new Keyframe()
              {
                  Opacity = "1.0"
              },
              ["to"] = new Keyframe()
              {
                  Opacity = "0"
              }
          };
      }
      //Media Query for Mobile Devices
      // @media (max-width: 480px) 
      [StylesheetMethod]
      void ForMobile() //Make body red for mobile phones
      {
          sheet[AtRuleType.MediaQuery] = new MediaQuery(new AtRule().MaxWidth("480px"))
          {
              ["body"] = new Element()
              {
                  BackgroundColor = "red"
              }
          };
      }
      // Media Query for low resolution  Tablets, Ipads
      // @media (min-width: 481px) and (max-width: 767px)
      [StylesheetMethod]
      void ForTablet() //Make body yellow for Tablets, Ipads
      {
          sheet[AtRuleType.MediaQuery] = new MediaQuery(new AtRule().MinWidth("481px").And.MaxWidth("767px"))
          {
              ["body"] = new Element()
              {
                  BackgroundColor = "yellow"
              }
          };
      }

      // Media Query for Laptops and Desktops
      // @media (min-width: 1025px) and (max-width: 1280px)
      [StylesheetMethod]
      void ForDesktop() //Make body gren for Laptops and Desktops
      {
          sheet[AtRuleType.MediaQuery] = new MediaQuery(new AtRule().MinWidth("1025px").And.MaxWidth("1280px"))
          {
              ["body"] = new Element()
              {
                  BackgroundColor = "green"
              }
          };
      }
  }
```

Apart from the style for the **NavBar.Razor**, we also added Media queries for **mobile**, **Tablet** and **Desktop** where we change the background color of the `body` for each. We also added animation called `flash` for` h1` elements.

That is how you can write you CSS in classes in C# using **BlazorStylesheet**. Following this way, you can have as many classes as you want. At the end of the day, all of them will be compiled into a single stylesheet file. 

> If you write same CSS in different places i.e classes, always the later ones will override the older ones. Depending the order of execution and compilation.

Here, I haven't covered a full tutorial on how to write CSS in C# using **BlazorStylesheet**. Because this library uses the **Stylesheet.NET** library, which has a comprehensive tutorial, you can refer to the link below for detailed guidance on writing CSS in C#.

**Repository**: https://github.com/AbdisamadMoh/Stylesheet.NET

### When and how to write CSS in components?

With **BlazorStylesheet**, we have the freedom to write our CSS anywhere in our Blazor project. This is handy for keeping styles specific to a particular component and updating our CSS in realtime. But remember, for bigger styles or styles used across your whole application, it's generally better to organize them in separate C# classes. This makes your code easier to manage.

Unlike classes, which we can access the **main stylesheet** through attribute decoration which the injection is managed by the **BlazorStylesheet** itself. In **components**, we can access the **main stylesheet** through injection provided by the **Dependency Injection (DI)** container provided by .NET.

In your components use `@Inject`. Put it top of your **razor** file.

```cshtml
@inject Stylesheet sheet
```

![image](https://imgur.com/Oy9LL3V.png)

Then you can use `sheet `in your component.

```csharp
 protected override void OnAfterRender(bool firstRender)
 {
     base.OnAfterRender(firstRender);
     if (firstRender)
     {
         sheet["body"].BackgroundColor = "blue";
         sheet.Build();
     }
 }
```

In **components** or other places where **BlazorStylesheet** doesn't manage, you have to call `sheet.Build()` when you change something in the stylesheet to reflect the changes.

> You don't need to call  `StateHasChanged()` as that has no effect on **BlazorStylesheet**.

### Why we need...

#### `loading="loader"` attribute in our `<html loading="loader">` and `fix.css`?

**BlazorStylesheet** depends on **JavaScript runtime (JSRuntime)** provided by Blazor. **JSRuntime** is not ready untill the whole page is loaded. This means **BlazorStylesheet** has to wait untill **JSRuntime** is ready to send the compiled CSS to the clientside.

This will create a problem where your website is not styled untill everything is loaded. You can fix this problem by either showing a loader or not displaying the website untill the css is ready and the website is styled.

Both solutions lies in `fix.css` file you added.

**To show loader while the website is getting ready add the following html attribute in your html tag.**

<html lang="en" loading="loader">

```html
<html loading="loader">
```

**To not show the content of the website while it is getting ready add the following html attribute in your html tag.**

```html
<html loading="">
```

<html lang="en" loading="loader">

<html lang="en" loading="loader">

> **BlazorStylesheet** does not add any element to the DOM to create the loader. So you should not worry about your DOM being modified.

## More...

This library uses **Stylesheet.NET** which you can find the repository below

**Repository**: https://github.com/AbdisamadMoh/Stylesheet.NET
