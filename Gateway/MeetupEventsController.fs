﻿module MeetupEventsController

open System
open MeetupEventsProxy
open DataTransform
open Models
open System.Net
open System.Net.Http
open System.Web.Http

type MeetupEventsController() =
    inherit ApiController()

    member this.Get(id : Guid) =
        let me = getMeetupEvent id
        match box me with
        | null -> this.Request.CreateResponse(HttpStatusCode.NotFound, "")
        | _ -> this.Request.CreateResponse(HttpStatusCode.OK, me)

    member this.Post(me: MeetupEvent) =
        let guid = PublishProxy.publishEvent me
        this.Request.CreateResponse(HttpStatusCode.Created, guid)

    member this.Delete(id : Guid) = 
        PublishProxy.deleteEvent id
        this.Request.CreateResponse(HttpStatusCode.NoContent)

    member this.Put(id : Guid) = 
        PublishProxy.updateEvent id
        this.Request.CreateResponse(HttpStatusCode.NoContent)
