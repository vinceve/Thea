# Thea

Thea is a (really) simple static site generator ideal for personal projects. It will take the content of your html files and inspect it for annotations. Based on these annotations, it will generate a complete static website.

## Philosophy

Thea was made out of the idea that a lot of people are making static websites but don't want to update each page whenever a customer asks for a small change. The goal was also to make it as easy as possible to develop on a Windows PC and to have almost no external dependencies. It also needs to be easy to test your built website in the browser without each time needing to compile.

## Getting started

- Download the exe file or build it from scratch.
- Read up about its usage

## Usage

### Configure

Download the exe and configure it on your path. To test if Thea is installed correctly, you can open a command prompt and run the help command.

```
$ thea help
```


### New project

To start a new project you need to generate a small folder structure containing a data folder and an output folder. In the data folder you will have 2 subfolders named _layouts and _partials. These will provide home for layout and partial files. To automatically generate this structure, you can use the following command.

```
$ thea generate new <projectname>
```

### Local server

Thea also has a local server. This is used to test out your static website. It will auto magically parse your files on the request made to the server. You can use it by going into your project folder and running:

```
$ thea serve
```

This will start a server on http://localhost:5000.

### Annotations

Currently there are the following annotations.

#### Layout

To use a custom layout that you can share across pages you can define a layout file into the _layouts folder and add the following annotation on the first line of your html file.

```
<!-- layout: my_layout.html -->
```

This defines that the content inside the html file needs to be wrapped with the my_layout.html file.

#### Yield

This annotation can only be used in a layout file. It will define where the content needs to be included in the layout.

```
<!-- yield -->
```

#### Partial

It's possible to define a partial and include it in a page.

```
<!-- partial: my_partial.html -->
```

This will take the file my_partial.html out of the _partials folder and include it on the spot where you defined it.

### Help

More information about Thea you will find if you run:

```
$ thea help
```

## Alpha/ todo

The current state of Thea should do what it supposed to do but isn't feature complete.

Todo:

- Custom port option
- cleanup code
- Example app
- add tests
- generate default site
