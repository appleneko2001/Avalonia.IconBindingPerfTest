# Avalonia.IconBindingPerfTest

This repository provides the source codes of icons value binding performances test.
Avalonia icons library are using https://github.com/AvaloniaUtils/Material.Icons.Avalonia

### About this test
I'm very care about performances of binding value in AvaloniaUI. WPF gave me an bad experiences because binding in WPF are a lot slower than AvaloniaUI, and I thinking about performances when changes icons.
so I created an simple project in my PC. Since this is my first time to create project about performances test, could contains some issues. Any suggestions to improve tests are welcome!

I taked widget from Material.Icons.Avalonia, named MaterialIcon. This widget using templated binding with converter.
and another widget are from [Material.Avalonia commit](https://github.com/AvaloniaUtils/material.avalonia/commit/1144986f17fc66ca3f75aa909f490aeaf4bbe168), named PackIcon. It uses binding but no converter. 

### My RIGS
Processor: AMD Ryzen 3 1200 Quad-Core<br/>
RAM: Patrick Viper 2x4GB 2666MHz<br/>
Graphics card: nVIDIA GeForce GTX 1050 Ti 4GB<br/>
### Results (lower is better):
![](/Avalonia.PackIconPerfTest/pics/results.png)

As you can see converter are much faster when a few changes happen, and binding without converter will faster when changes are too much, especially 10000 times changes. But in real cases, a lot changes that higher than 100 is almost impossible, it's too EXTREMELY RARE TO HAPPEN, so binding with converter are better in this test.

Since WPF gaves me bad experiences in binding and binding with converters, I have some negative opinion of binding. But after this tests, I think AvaloniaUI's binding are much better than WPF on performances and conveniences. Recently I'm using .NET Core with AvaloniaUI Framework to create my applications, bindings is often used features in my works. AvaloniaUI's binding are more comfortable than WPF, because it gaves you binding with simplest way, even functions binding. 
