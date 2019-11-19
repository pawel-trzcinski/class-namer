ClassNamer

127.0.0.1/RandomClassName Every GET pulls random class name
	- html page for Accept:text/html
	- plain text for anything else
127.0.0.1/Test

docker build -t class_namer:latest .

configuration file:
- words can duplicate - no propability impact - Distinct used