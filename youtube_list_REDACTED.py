# -*- coding: utf-8 -*-

# Sample Python code for youtube.search.list
# See instructions for running these code samples locally:
# https://developers.google.com/explorer-help/code-samples#python

import datetime
import os

#import google_auth_oauthlib.flow
import googleapiclient.discovery
import googleapiclient.errors

scopes = ["https://www.googleapis.com/auth/youtube.force-ssl"]

def main():
    # Disable OAuthlib's HTTPS verification when running locally.
    # *DO NOT* leave this option enabled in production.
    os.environ["OAUTHLIB_INSECURE_TRANSPORT"] = "1"

    api_service_name = "youtube"
    api_version = "v3"

    # Create developer key at google and use on a line below.
    youtube = googleapiclient.discovery.build(api_service_name, api_version, developerKey='googledevv-developerkeyy-GOOGLEDEVELOPE');
    
    request = youtube.search().list(
        part="snippet",
        maxResults=1,
        publishedAfter=(datetime.datetime.now()+datetime.timedelta(seconds=-180)).strftime('%Y-%m-%dT%H:%M:%SZ'),
        q="a"
    )
    response = request.execute()

    print(response["items"][0]["id"])
    print(response["items"][0]["snippet"])

if __name__ == "__main__":
    main()
