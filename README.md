[![NuGet](https://img.shields.io/nuget/v/Narumikazuchi.svg)](https://www.nuget.org/packages/Narumikazuchi)
![Tests](https://img.shields.io/github/actions/workflow/status/Narumikazuchi/Common/linux-tests.yml?label=linux-tests)
![Tests](https://img.shields.io/github/actions/workflow/status/Narumikazuchi/Common/windows-tests.yml?label=windows-tests)  

![Logo](../release/logo.png)  

# Narumikazuchi
This library contains some small utilities that I would use occasionaly but don't really fit a certain category. Some of my other libraries also use these types so they basically have been moved to a package for them to share and use.

## Table of Contents
- [Installation](#installation)
- [Types & Usage](#types--usage)
    - [Types](#types)
        - [AlphanumericVersion](#alphanumericversion)
        - [AttributeResolver](#attributeresolver)
        - [EqualityComparison&lt;TComparable&gt;](#equalitycomparisontcomparable)
        - [EventHandler&lt;TSender&gt; and EventHandler&lt;TSender, TEventArgs&gt;](#eventhandlertsender-and-eventhandlertsender-teventargs)
        - [FailedInitialization](#failedinitialization)
        - [ImpossibleState](#impossiblestate)
        - [INotifyPropertyChangedHelper and INotifyPropertyChangingHelper](#inotifypropertychangedhelper--inotifypropertychanginghelper)
        - [NotAllowed](#notallowed)
        - [Pointer&lt;TAny&gt;](#pointertany)
    - [Extensions](#extensions)
        - [IComparable&lt;TComparable&gt;](#icomparabletcomparable)
        - [String](#string)
    - [Types (Deprecated)](#types-deprecated)
        - [Converter&lt;TResult&gt; and IConvertible&lt;TResult&gt;](#convertertresult-and-iconvertibletresult)
        - [ExceptionInformation & FunctionCallInformation](#exceptioninformation-and-functioncallinformation)
    - [Extensions (Deprecated)](#extensions-deprecated)
        - [Exception](#exception)
- [For Contributors](#for-contributors)
- [Benchmarks](#benchmarks)
- [Roadmap](#roadmap)

## Installation
Just add a reference the the package and add the appropriate using statement ```using Narumikazuchi```.  
```
dotnet add package Narumikazuchi
```

## Types & Usage

### Types
#### AlphanumericVersion
This struct provides all the same functionality that is also provides by the ```System.Version``` class but additionally provides the ability to use alphabetic characters as well as a -(dash) character in the version descriptor. With this, version descriptors like ```1.0-alpha1``` or ```rc3-1.0.0``` are possible.

#### AttributeResolver
The ```AttributeResolver``` static class is a simple wrapper around the ```System.Reflection``` methods that are used to get specific ```Attribute``` classes from assemblies, types, methods, properties, fields or parameters, that are decorated with them. The results of the member methods will get cached and improve performance for the next time the method is called with the same parameter.  

HasAttribute&lt;TAttribute&gt; will check whether or not the object reflected by the passed parameter is decorated with an attribute of type ```TAttribute``` and will return ```true``` if at least one attribute of that type is found and otherwise ```false```.
```csharp
static public System.Boolean HasAttribute<TAttribute>(System.Reflection.Assembly assembly)
    where TAttribute : System.Attribute;
static public System.Boolean HasAttribute<TAttribute>(System.Reflection.MemberInfo info)
    where TAttribute : System.Attribute;
static public System.Boolean HasAttribute<TAttribute>(System.Reflection.ParameterInfo info)
    where TAttribute : System.Attribute;
```  
  
FetchFetchAllAttributes&lt;TAttribute&gt; will collect all instances of the attributes of type ```TAttribute``` the object reflected by the passed parameter is decorated with and return them as an ```ImmutableArray<TAttribute>``` to the caller.
```csharp
static public System.Collections.Immutable.ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(System.Reflection.Assembly assembly)
    where TAttribute : System.Attribute;
static public System.Collections.Immutable.ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(System.Reflection.MemberInfo info)
    where TAttribute : System.Attribute;
static public System.Collections.Immutable.ImmutableArray<TAttribute> FetchAllAttributes<TAttribute>(System.Reflection.ParameterInfo info)
    where TAttribute : System.Attribute;
```  
  
FetchFetchSingleAttribute&lt;TAttribute&gt; might fail with an ```NotAllowed``` exception if the object reflected by the passed parameter is not decorated with an attribute of type ```TAttribute``` or if ```[AttributeUsage(..., AllowMultiple = true)]class TAttribute {...}```. Otherwise this method will return the only instance of ```TAttribute``` the parameter is decorated with.
```csharp
static public TAttribute FetchSingleAttribute<TAttribute>(System.Reflection.Assembly assembly)
    where TAttribute : System.Attribute;
static public TAttribute FetchSingleAttribute<TAttribute>(System.Reflection.MemberInfo info)
    where TAttribute : System.Attribute;
static public TAttribute FetchSingleAttribute<TAttribute>(System.Reflection.ParameterInfo info)
    where TAttribute : System.Attribute;
```

#### EqualityComparison&lt;TComparable&gt;
This delegate got added since there is a ```System.Comparison<T>``` delegate to compare for ordering for types that do not implement the ```System.IComparable<T>``` interface but no delegate as equivalent for types that do not implement the ```System.IEquatable<T>``` interface.  
This might get deprecated once .NET 9 is out with the increases utility for extension methods, since the last time I checked you could just 'extend' a type to implement an interface that the original type currently does not, which would make this delegate basically useless. You can look [here](https://github.com/dotnet/csharplang/blob/main/proposals/extensions.md) if you don't know what I'm talking about.

#### EventHandler&lt;TSender&gt; and EventHandler&lt;TSender, TEventArgs&gt;
I added these delegates since the integrated ```System.EventHandler``` and ```System.EventHandler<T>``` delegates use the ```System.Object``` type for the sender in the method signature. With these 2 new delegates the will be no need to seperately cast the ```sender``` parameter on the delegate.

#### FailedInitialization
Sometimes you decide to use a ```struct``` instead of a ```class``` not because of the 'value-type' nature of what you want to implement, but rather the performance that it can bring, especially when the resulting struct in unmanaged. However that might also mean that the struct might be in an invalid state, due to being created with the ```default``` expression. This was the motivation behind this exception. However due to how it is named it can also be used in other scenarios and can provide literal feedback just through it's name alone that something during initialization of something failed.

#### ImpossibleState
This one is mainly an exception for code readability. It might not happen often, but there are times when you just need to satisfy the compiler by throwing an exception while knowing fully well, that this exception will never get hit.  

#### INotifyPropertyChangedHelper and INotifyPropertyChangingHelper
The origin of these interfaces was when I started implementing extension methods for collections. Because there were observable collections among them I needed a way to trigger the ```PropertyChanged``` and ```PropertyChanging``` events from outside of the class or struct. Right now I think this is bad design and am contemplating whether to deprecate these interfaces or not, but for now the are still here I guess.

#### NotAllowed
My motivation for this exception is simple readability. Sometimes it is just easier when the first word you are reading already tells you what the problem was. I'm also aware that readability is subjective. I personally don't like the suffix 'Exception' because there is no other class or struct you can use in a ```throw``` statement and with the correct naming you can easily understand what problem occured. The name might also not be quite ideal for now, so it might change in the future. I'm alway open for suggestions on that part.

#### Pointer&lt;TAny&gt;
In rare cases one might need a pointer in C#, but who likes enabling unsafe context just for a few lines for the whole assemply? This struct provides a managed way to use pointers. It implicitly casts from and to ```System.IntPtr```, ```System.UIntPtr``` and an unsafe pointer type. It provides the ability to go through an array-like structure, meaning you can perform pointer-arithmetic on it. This is obviously still highly unsafe if used uncorrectly, so use with care.  
  
The AddressOf method serves as the primary way to construct a ```Pointer<TAny>``` struct. Use this to get the pointer to any object you need the pointer of.
```csharp
static public Pointer<TAny> AddressOf(ref TAny t);
```  
  
If for some reason you only have regular pointers, you can transform them into a ```Pointer<TAny>``` with on of these constructors.
```csharp
public Pointer(void* pointer);
public Pointer(IntPtr pointer);
public Pointer(UIntPtr pointer);
```  
  
Using the indexer will offset the pointer. The offset will be ```index``` * the width of the pointer. This basically only useful for scanning arrays.
```csharp
public TAny this[Int32 index];
```  
  
Incase you need the address of the pointer you can get it with the ```Address``` property. But then again, you can also just implicitly cast it, if you want to.
```csharp
public System.UIntPtr Address
{
    get;
}
```  
  
Through this property you can get or set the value of the object at the pointers address. Be careful not to set any data where it doesn't belong.
```csharp
public TAny Value
{
    get;
    set;
}
```

### Extensions
#### IComparable&lt;TComparable&gt;
Instead of only having ```Math.Clamp``` for primitive numeric types you can now use the ```SystemExtension.Clamp<TComparable>``` method on every class or struct that implements the ```System.IComparable<T>``` interface.  
```csharp
static public TComparable Clamp<TComparable>(this TComparable value, 
                                             TComparable lowerBoundary,
                                             TComparable higherBoundary) 
    where TComparable : System.IComparable<TComparable>;
```  
  
There are also 3 methods to check for boundaries and throw an ```ArgumentOutOfRangeException``` should the object not be within the specified bounds. Be aware that all of these methods expect neither the ```source``` nor ```boundary```, ```lowerBoundary``` or ```higherBoundary``` to be ```null```.
```csharp
static public void ThrowIfLesserThan<TComparable>(this TComparable source,
                                                  TComparable boundary,
                                                  System.String message = default,
                                                  [CallerArgumentExpression(nameof(source))] System.String? paramName = "")
    where TComparable : System.IComparable<TComparable>;
static public void ThrowIfBiggerThan<TComparable>(this TComparable source,
                                                  TComparable boundary,
                                                  System.String message = default,
                                                  [CallerArgumentExpression(nameof(source))] System.String? paramName = "")
    where TComparable : System.IComparable<TComparable>;
static public void ThrowIfOutOfRange<TComparable>(this TComparable source,
                                                  TComparable lowerBoundary,
                                                  TComparable higherBoundary,
                                                  System.String message = default,
                                                  [CallerArgumentExpression(nameof(source))] System.String? paramName = "")
    where TComparable : System.IComparable<TComparable>;
```

#### String
If you have a string and want to use it as a filename while either expecting or not knowing if it contains invalid characters to be used as filename, then you can use ```SystemExtension.SanitizeForFilename```. This will just omit any invalid characters from the resulting string, so that it can be used as filename.  
This will either get superceded by the planned ```FilePath``` type or it will get expanded upon with another overload to specify a character to replace an invalid one.
```csharp
static public String SanitizeForFilename(this String raw);
```  
  
You can also use an extension method to check whether the ```System.String``` is ```null``` or whitespace. There are plans to improve the workings for this method but for now it can either throw an ```ArgumentNullException``` or a ```NullReferenceException``` depending on whether the ```asArgumentException``` parameter is set or not.
```csharp
static public void ThrowIfNullOrEmpty(this System.String? source,
                                      System.String message = default,
                                      Boolean asArgumentException = default,
                                      [CallerArgumentExpression(nameof(source))] System.String? varName = "")
```

### Types (Deprecated)
#### Converter&lt;TResult&gt; and IConvertible&lt;TResult&gt;
The ```Converter<TResult>``` static class was originally planned to an easy one-liner for conversion of a type that implements the ```IConvertible<TType>``` interface. There is now a 'Narumikazuchi.Generators.Convertibles' source generator in the works which will supercede the ```Converter<TResult>``` class and ```IConvertible<TType>``` interface.

#### ExceptionInformation and FunctionCallInformation
These types were originally supposed to help with logging and analyzing failures in an application. However I never found myself actually using them or even thinking of their usefulness. Thus I have made the decision, that the current functionality of exceptions is enough to analyze any failures in logging.

### Extensions (Deprecated)
#### Exception
There was just an extension method ```ExtractInformation(this Exception source)```, whose job was to access the ```ExceptionInformation``` which was supposed to make exception more logging-friendly and readable. Since that type is now deprecated the method is also no longer needed.

## For Contributors
- Group files into folders with either similar meaning or covering topics of a similar namespace (i.e. Reflection)
- **DO NOT** add sub-namespaces even for files in folders. The entire project used one namespace unless explicitly stated otherwise
- Only one type per file
- Filenames should equal the typename. If the type is generic use the following naming scheme ```Typename`NumberOfTypeParameters.cs```
- Keep files to less than 300 lines of code
- Only 1 method call per line. If you chain method calls, align them at the dot (.) of the previous method call
- Split compilation units into multiple files if the type for that CU implements interfaces. Create a file for each implemented interface with the naming scheme of ```Typename.Interface`NumberOfTypeParameters.cs```, if the type is non-generic the backtick (`) and the number of type parameters should be omitted
- Should the number of lines for private members and their implementations exceed ```25 lines``` create a file with the naming scheme ```Typename.Private.cs``` and move private members into there
- Should the type contain nested types create a seperate file for the nested type with the naming scheme ```Typename.NestedTypename.cs```
- Use #pragma to supress undesired warnings
- **DO NOT** add ```using System.*``` to a file. Add usings from the ```System``` namespace and it's sub-namespaces into the .csproj file under ```<ItemGroup><Using Include="System.*" /></ItemGroup>``` at the bottom of the file
- Provide tests for every implemented type. Use sentence-like naming to express what is expected to happen given specific circumstances (i.e. ```class A_foo.should_return_42.if_instantiated_using_bar()```)
- Tests should be implemented in the ```Tests``` project. Group the tests for your types according to where or for what it is expected to be used
- Provide benchmarks for every implemented type. If applicable provide comparisons to native functionality.
- Benchmarks should be implemented in the ```Benchmarks``` project. Group the benchmarks for your types in the same way they are grouped in the main project

## Benchmarks
The ```Benchmarks``` project should be set as the default startup project in visual studio. Since there is no Debug configuration for the project you can just start the project directly from the IDE.  
Alternatively you can run the benchmarks using the CLI:
```
dotnet run --project **YOUR_SOLUTION_LOCATION**/Benchmarks/Benchmarks.csproj
```

## Roadmap
The following actions are planned for the upcoming version ```6.1.0```:  
- Add a ```StaticEventHandler<TEventArgs>``` delegate for static classes or just events where the sender is irrelevant
- Implement more granular versions of the ```ThrowIfNullOrEmpty``` extension method for ```System.String```
- Check for performance improvements in ```SanitizeForFilename(this String raw)``` and ```AttributeResolver```
- Add an overload for ```SanitizeForFilename(this String raw)``` where you can pass in the ```System.Char``` that should replace invalid characters instead of just omitting them
- Adding a ```FilePath``` type which checks and replaces invalid characters in a string directly (might also get moved to ```Narumikazuchi.InputOutput```)
- Incase ```FilePath``` will put into ```Narumikazuchi.InputOutput``` -> also move ```SanitizeForFilename(this String raw)```