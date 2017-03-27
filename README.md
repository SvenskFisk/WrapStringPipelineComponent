# WrapStringPipelineComponent
BizTalk component for working with messages as strings inside an orchestration.

By adding these components to the receive and/or send pipelines you can use messages of type System.String inside of ochestrations. This is primarily useful for processing the message in a helper class. The componets wrap/unwrap the message with `<string><![CDATA[` and `]]></string>` thus allowing the orchestration engine to recognize the messagetype and parse them as xml.

This is not a streaming component, avoid very big files! I will change it to streaming in the future.