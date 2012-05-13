<?php $facebook = new Facebook(array( 'appId' =>"YOURAPPID",
	  'secret' => "APPSECRET",
	  	  'cookie' => false
		  	));
			 
			  
			  if ($me) {
			  	$logoutUrl =   $facebook->getLogoutUrl();
				} else {
					$loginUrl   =   $facebook->getLoginUrl(array(
							   'canvas'    => 0,
							   		   'fbconnect' => 1,
									   		   'req_perms' => 'publish_stream,status_update,offline_access,publish_checkins'
											   		   ));
													   }
													   ?>
													   <? if ($me){?>
													   	Thanks- application has been accepted
														<? }else{ ?>
														 
														 	<a href="<?= $loginUrl; ?>">
																Login into Facebook to accept applicatioon
																	 </a>
																	  
																	  <?}?>
																	   
																	    
																	    <?   $url = "https://graph.facebook.com/oauth/access_token"; 		 
																	    	 $client_id = "client_id"; 		  
																		 	 $client_secret = "app_id"; 		  
																			 	 $postString = "client_id=$client_id&client_secret=$client_secret&type=client_cred&scope=email,publish_stream,offline_access,publish_checkins"; 		 
																				 //This first bit of code is to get the application a access token.
																				 $curl = curl_init(); 		  curl_setopt($curl, CURLOPT_URL, $url); 		  curl_setopt($curl, CURLOPT_FAILONERROR, 1); 		  curl_setopt($curl, CURLOPT_RETURNTRANSFER, 1); 		  //curl_setopt($curl, CURLOPT_FOLLOWLOCATION, 1); 		  curl_setopt($curl, CURLOPT_POST, 1); 		  curl_setopt($curl, CURLOPT_POSTFIELDS, $postString); 		  $response = curl_exec($curl); 	 		 		 $url = "https://graph.facebook.com/".$FBID."/checkins"; 		 $token = substr($response,13); 		   		  $attachment = array( 		   'access_token' =-->  $token,
																				 		   'place' => 'place_ID',
																						   		   'message' =>'I went to placename today',
																								    
																								    		   'picture' => 'http://logo for post/',
																										   		   'coordinates' => json_encode(array(
																												   			'latitude'  => 'lat',
																																		'longitude' => 'long',
																																					'tags' => "")));//tag a user to be tagged in as well
																																					 
																																					 		$attachment =	$attachment;
																																									  print_r($attachment);
																																									   
																																									   		  $ch = curl_init();
																																											  		  curl_setopt($ch, CURLOPT_URL,$url);
																																													  		  curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, FALSE); // This i added as the URL is https
																																															  		  curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, 2);      // This i added as the URL is https
																																																	  		  curl_setopt($ch,CURLOPT_RETURNTRANSFER,1);
																																																			  		  curl_setopt($ch, CURLOPT_POST, true); // Do I need this ?
																																																					  		  curl_setopt($ch, CURLOPT_POSTFIELDS, $attachment);
																																																							  		  $result= curl_exec($ch);
																																																									  		  curl_close ($ch);
																																																											  		  print_r($result);// some debug info
																																																													   
																																																													   ?>

