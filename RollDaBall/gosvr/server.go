package main

import "net"
import "fmt"
import "bufio"
import "strings" // only needed below for sample processing
func main() {
	fmt.Println("Launching server...") // listen on all interfaces

	ln, _ := net.Listen("tcp", ":8081") // accept connection on port
	defer ln.Close()
	for {
		conn, _ := ln.Accept()
		go func(c net.Conn) {
			for { // will listen for message to process ending in newline (\n)
				message, _ := bufio.NewReader(c).ReadString('\n') // output message received
				fmt.Print("Message Received:", string(message))   // sample process for string received
				newmessage := strings.ToUpper(message)            // send new string back to client
				c.Write([]byte(newmessage + "\n"))
			}
		}(conn)
	}
}
