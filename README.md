Sends log messages to the [log.io](http://logio.org/).

##Configuration Syntax
```xml

<nlog> 
  <extensions> 
    <add assembly="NLog.Targets.LogIO"/> 
  </extensions> 
  <targets>
    <target xsi:type="LogIO"
            name="String"
            domain="String" 
            port="Integer"
            node="Layout"
            stream="Layout"
            layout="Layout"
            onOverflow="Enum"
            maxMessageSize="Integer"
            maxQueueSize="Integer"
    />
    </targets>
</nlog>

```

## Install log.io Server on Windows

1) Install via npm

    npm install -g log.io

2) Run server

    log.io-server.cmd

3) Browse to http://localhost:28778
