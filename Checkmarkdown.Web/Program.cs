// Load template from file

using HandlebarsDotNet;
var templatePath = Path.Combine("Resources", "html", "index.hbs");
var templateContent = File.ReadAllText(templatePath);

// Compile template
var template = Handlebars.Compile(templateContent);

// Data model
var data = new {
    person = new { name = "Alice" },
};

// Render
var result = template(data);
Console.WriteLine(result);