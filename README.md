# WrapStringPipelineComponent
BizTalk component for working with messages as strings inside an orchestration.

By adding this to the receive and/or send pipelines you can use messages of type System.String inside of ochestrations. This is primarily useful for processing the message inside of a helper class.

These components wrap the message with *"<string><![CDATA["* and *"]]></string>"* thus allowing the orchestration engine to rcognize the messagetype and parse them as xml.