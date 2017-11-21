# WrapStringPipelineComponent
BizTalk component for working with messages as strings inside an orchestration.

Allows using messages of type System.String inside of ochestrations. The componets wrap/unwrap the message with `<string><![CDATA[` and `]]></string>` thus allowing the orchestration engine to recognize the message type and parse them as xml.

This is not a streaming component, avoid very big files! I will change it to streaming in the future.
