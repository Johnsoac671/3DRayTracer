# 3DRayTracer

This project is a simple Ray Tracing rendering tool written in C\#. It is based on the book "[Ray Tracing in One Weekend](https://raytracing.github.io/books/RayTracingInOneWeekend.html)" by Peter Shirley, Trevor David Black, and Steve Hollasch. The goal of this project is to create a rendering engine capable of producing photorealistic images.

## Features

The rendering engine currently supports the following features:

  * **Primitives**:
      * **Spheres**: The engine can render sphere primitives. It also supports motion blur for spheres by defining start and end locations.
  * **Materials**:
      * **Diffuse**: Matte, non-reflective surfaces.
      * **Metal**: Reflective, metallic surfaces with adjustable roughness.
      * **Dielectric**: Transparent materials like glass that refract and reflect light.
  * **Camera Effects**:
      * **Depth of Field**: The camera can simulate depth of field with a configurable focus angle and distance.
  * **Output**:
      * The final rendered image is saved in the **PPM (Portable Pixmap)** format.
  * **Architecture**:
      * The solution is divided into three projects: a `Rendering Engine` class library, a `Frontend` console application to drive the renderer, and a `Tests` project for unit testing the core utilities.

## Example of a Producible Image

<img width="1117" height="627" alt="raytracing" src="https://github.com/user-attachments/assets/937b6e2f-3783-4b81-98eb-d629dad4510f" />

## Getting Started

Follow these instructions to get a copy of the project up and running on your local machine.

### Prerequisites

This is a .NET 8 project. You will need to have the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine.

### Running the Application

1.  Clone the repository to your local machine.

2.  Navigate to the root directory of the project in your terminal.

3.  Run the following command:

    ```bash
    dotnet run --project Frontend
    ```

4.  This will start the rendering process. Once it's complete, an `output.ppm` file will be generated in the `Frontend/bin/Debug/net8.0` directory. You can view this file with an image viewer that supports the PPM format, such as [IrfanView](https://www.irfanview.com/) or [GIMP](https://www.gimp.org/).

Alternatively, you can open the `3DRayTracer.sln` file in Visual Studio and run the `Frontend` project.

## License

This project is licensed under the **MIT License**. See the [LICENSE](https://www.google.com/search?q=LICENSE) file for details.
