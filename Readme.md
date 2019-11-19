# ClassNamer

### Using
* **_127.0.0.1:15400/RandomClassName_** Every GET pulls random class name
  - html page for Accept:text/html
  - plain text for anything else
* **_127.0.0.1:15400/Test_**

### Building
```
docker build -t class_namer:latest .
```

### Mics
* configuration file:
  - words can duplicate - no propability impact - Distinct used